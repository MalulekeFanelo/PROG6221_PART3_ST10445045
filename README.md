🛡️ SecuBot – Cybersecurity Awareness Assistant (WPF Edition)
SecuBot is an interactive WPF-based chatbot assistant built in C#, designed to promote cybersecurity awareness through natural conversation, reminders, and gamified learning. It simulates basic natural language understanding and empowers users to stay cyber smart through personalized tips, tasks, and quizzes.

Unlisted Youtube link: https://youtu.be/31jLUG549KY 

🧠 Core Features
💬 Natural Conversations
Personalized greetings using your name and emotional state

Sentiment-aware responses to moods like curious, worried, frustrated, happy, overwhelmed, bored, confident, etc.

Context-aware clarifications (e.g., "I’m confused", "Tell me more")

Understands follow-up queries using retained context

🔐 Cybersecurity Topic Intelligence
SecuBot understands 12+ key cybersecurity keywords, including:

password, phishing, privacy, scam, malware, antivirus, firewall,
encryption, two-factor, updates, spam, social engineering

Each comes with randomized, real-world safety tips and friendly guidance.

📋 Task Management System
SecuBot includes an interactive task manager where you can:

✅ Add, complete, and delete custom cybersecurity tasks

⏰ Set reminders (1 day, 3 days, 1 week, 2 weeks, or custom)

🔁 Add tasks via chat (e.g., "Remind me to update my password in 3 days")

💬 Receive follow-up prompts for setting reminders (e.g., "Would you like to set a reminder for this task?")

🧠 Quiz Mini-Game
Test your knowledge with the built-in Cybersecurity Quiz, featuring:

Randomly selected multiple-choice questions

Score tracking

Activity log updates on quiz start and completion

📜 Activity Log
SecuBot automatically logs your interactions:

Tasks added, deleted, or marked complete

Reminders set

Quizzes started and completed (with scores)

Topics accessed or clarified

Mood setting and conversation flow

🔎 Shows the 5 most recent actions with a “Show More” option to view the full history.

🎧 Voice Greeting
SecuBot plays an audio greeting when the app starts, giving it a warm, human-like touch (optional, configurable).

🖥️ Interface Highlights
Built using WPF (Windows Presentation Foundation):

Clean UI with sections for ASCII art, chat history, user input, and buttons

Dialog windows for Task Manager, Quiz, and Activity Log

Auto-scroll chat area with styled text and consistent message formatting

📂 Folder & File Structure
markdown
Copy
Edit
📁 SecurityCyberBot
├── ChatBot.cs
├── CybersecurityTask.cs
├── QuizQuestion.cs
├── AudioPlayer.cs
├── ImageDisplay.cs
├── MainWindow.xaml / .xaml.cs
├── TaskWindow.xaml / .xaml.cs
├── QuizWindow.xaml / .xaml.cs
├── LogWindow.xaml / .xaml.cs
└── Resources/
    └── ChatBotVoice.wav
🚀 Getting Started
Requirements
.NET 6 or .NET 9 SDK

Visual Studio 2022+ (or any WPF-capable IDE)

Windows OS (for full WPF compatibility)

Steps
Clone the repository

Open the solution in Visual Studio

Ensure ChatBotVoice.wav is included and set to Copy if newer

Run the application (F5 or dotnet run)

🔍 Example Interaction
vbnet
Copy
Edit
👤 User: I'm feeling overwhelmed.
🤖 Bot: Cybersecurity can be complex, but we’ll break it down step by step. 🤝

👤 User: Add a task to enable two-factor authentication.
🤖 Bot: Task added: 'Enable two-factor authentication'. Would you like to set a reminder?

👤 User: Yes
🤖 Bot: When should I remind you? (1 day, 3 days, 1 week, 2 weeks, or custom)

👤 User: In 3 days
🤖 Bot: Reminder set for 30 June 2025. ✅
