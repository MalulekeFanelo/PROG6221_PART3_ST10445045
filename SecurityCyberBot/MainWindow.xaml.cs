using System.Windows;
using System.Windows.Input;
using SecurityCyberBot.Utilities;

namespace SecurityCyberBot
{
    public partial class MainWindow : Window
    {
        private ChatBot chatbot;
        private AudioPlayer audioPlayer = new AudioPlayer();
        private ImageDisplay imageDisplay = new ImageDisplay();

        private string currentPhase = "askName"; // Phases: askName → askMood → chat
        private string userName = "";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            audioPlayer.PlayGreeting();
            AsciiArtDisplay.Text = imageDisplay.GetAsciiArt();
            AddMessage("👋 Hello! What's your name?");
        }

        private void AddMessage(string message)
        {
            ChatDisplay.Items.Add(message);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessInput();
            }
        }

        private void ProcessInput()
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AddMessage($"You: {input}");
            UserInput.Clear();

            string response = "";

            if (currentPhase == "askName")
            {
                userName = input;
                chatbot = new ChatBot(userName);
                currentPhase = "askMood";
                response = $"Hi {userName}, how are you feeling about cybersecurity today?";
            }
            else if (currentPhase == "askMood")
            {
                response = chatbot.ProcessInput(input); // mood is stored inside
                currentPhase = "chat";
                AddMessage($"Bot: {response}");
                response = "Ask me something about cybersecurity:";
            }
            else
            {
                response = chatbot.ProcessInput(input);
            }

            AddMessage($"Bot: {response}");

            // Handle special windows
            if (response.Contains("Opening task manager"))
            {
                var taskWindow = new TaskWindow(chatbot);
                taskWindow.Owner = this;
                taskWindow.ShowDialog();
            }
            else if (response.Contains("Opening cybersecurity quiz"))
            {
                var quizWindow = new QuizWindow(chatbot.QuizQuestions);
                quizWindow.Owner = this;
                quizWindow.ShowDialog();

                chatbot.QuizScore = quizWindow.Score;
                chatbot.ActivityLog.Add($"✅ Quiz completed. Score: {quizWindow.Score}/{chatbot.QuizQuestions.Count}");
            }

        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new TaskWindow(chatbot);
            taskWindow.Owner = this;
            taskWindow.ShowDialog();
        }

        private void QuizButton_Click(object sender, RoutedEventArgs e)
        {
            var quizWindow = new QuizWindow(chatbot.QuizQuestions);
            quizWindow.Owner = this;
            quizWindow.ShowDialog();

            chatbot.QuizScore = quizWindow.Score;
            chatbot.ActivityLog.Add($"✅ Quiz completed. Score: {quizWindow.Score}/{chatbot.QuizQuestions.Count}");
        }


        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            var logWindow = new LogWindow(chatbot.ActivityLog);
            logWindow.Owner = this;
            logWindow.ShowDialog();
        }
    }
}
