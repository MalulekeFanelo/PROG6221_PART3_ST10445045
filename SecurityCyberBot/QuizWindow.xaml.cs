using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCyberBot
{
    public partial class QuizWindow : Window
    {
        private int currentQuestionIndex = 0;
        private int score = 0;
        private List<QuizQuestion> questions;
        private List<int> userAnswers = new List<int>();

        public int Score => score;

        public QuizWindow(List<QuizQuestion> quizQuestions)
        {
            InitializeComponent();
            questions = quizQuestions;
            userAnswers = Enumerable.Repeat(-1, questions.Count).ToList();
            LoadQuestion(0);
        }

        private void LoadQuestion(int index)
        {
            if (index < 0 || index >= questions.Count) return;

            currentQuestionIndex = index;
            var currentQuestion = questions[index];

            QuestionText.Text = $"{index + 1}. {currentQuestion.Question}";
            FeedbackText.Text = "";

            // Clear previous options
            OptionsPanel.Children.Clear();

            // Add RadioButtons dynamically for each option
            for (int i = 0; i < currentQuestion.Options.Count; i++)
            {
                var optionText = currentQuestion.Options[i];
                var rb = new RadioButton
                {
                    Content = optionText,
                    GroupName = "QuizAnswers",
                    Margin = new Thickness(5),
                    FontSize = 14,
                    Padding = new Thickness(5)
                };

                // Restore previous selection if any
                if (userAnswers[index] == i)
                    rb.IsChecked = true;

                rb.Checked += Option_Checked;

                OptionsPanel.Children.Add(rb);
            }

            // Update navigation buttons
            PrevButton.IsEnabled = (index > 0);
            NextButton.IsEnabled = (index < questions.Count - 1);
            SubmitButton.Visibility = (index == questions.Count - 1) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            var selectedOption = sender as RadioButton;
            if (selectedOption == null) return;

            // Save answer when an option is selected
            var currentQuestion = questions[currentQuestionIndex];
            int selectedIndex = currentQuestion.Options.IndexOf(selectedOption.Content.ToString());
            userAnswers[currentQuestionIndex] = selectedIndex;

            // Show feedback
            if (selectedIndex == currentQuestion.CorrectAnswerIndex)
            {
                FeedbackText.Text = $"Correct! {currentQuestion.Explanation}";
                FeedbackText.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                FeedbackText.Text = $"Incorrect. {currentQuestion.Explanation}";
                FeedbackText.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestion(currentQuestionIndex - 1);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            LoadQuestion(currentQuestionIndex + 1);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if current question has an answer
            if (userAnswers.Contains(-1))
            {
                MessageBox.Show("Please answer all questions before submitting.", "Incomplete Quiz", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CalculateScore();

            MessageBox.Show($"Quiz finished! You scored {score} out of {questions.Count}.", "Quiz Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CalculateScore()
        {
            score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (userAnswers[i] == questions[i].CorrectAnswerIndex)
                {
                    score++;
                }
            }
        }
    }
}
