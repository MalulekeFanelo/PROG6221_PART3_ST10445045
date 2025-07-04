﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityCyberBot
{
    public class ChatBot
    {
        private readonly string userName;
        private bool isRunning = true;
        private string userMood = "";
        private string favouriteTopic = "";
        private string currentTopic = "";
        private bool awaitingReminderConfirmation = false;
        private CybersecurityTask pendingTask = null;
        private bool awaitingReminderDate = false;


        public List<CybersecurityTask> Tasks { get; } = new List<CybersecurityTask>();
        public List<QuizQuestion> QuizQuestions { get; } = new List<QuizQuestion>();
        public int QuizScore { get; set; }
        public List<string> ActivityLog { get; } = new List<string>();

        private Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use strong, unique passwords for each account.",
                "Never use personal info like your name or birthdate in passwords.",
                "Consider using a password manager to keep track of complex passwords.",
                "Avoid using the same password across multiple platforms.",
                "Change your passwords regularly, especially for sensitive accounts."
            }},
            { "phishing", new List<string> {
                "Never click on suspicious links. Hover to preview the URL first.",
                "Look for grammar mistakes and odd email addresses in suspicious emails.",
                "Banks and legit services won't ask for passwords via email.",
                "Verify the sender's identity before sharing any information.",
                "Check for mismatched URLs that look similar to trusted domains."
            }},
            { "privacy", new List<string> {
                "Review app permissions and privacy settings regularly.",
                "Avoid oversharing on social media. Scammers love personal info.",
                "Use two-factor authentication for sensitive accounts.",
                "Turn off location tracking when not needed.",
                "Be cautious when using public Wi-Fi to access private accounts."
            }},
            { "scam", new List<string> {
                "If it sounds too good to be true, it probably is.",
                "Scammers often create urgency. Always take a moment to think.",
                "Don't trust messages asking for money or gift cards out of the blue.",
                "Verify the identity of anyone asking for sensitive info.",
                "Look out for fake competitions or 'you've won' messages."
            }},
            { "malware", new List<string> {
                "Malware can hide in downloads, so always use trusted sources.",
                "Install antivirus software to detect and remove malware.",
                "Avoid clicking on suspicious ads or pop-ups — they often carry malware.",
                "Don't download pirated software or media — they can include malware.",
                "Keep your operating system and apps updated to close security holes."
            }},
            { "antivirus", new List<string> {
                "Keep your antivirus software up to date for best protection.",
                "Run regular scans to detect hidden threats.",
                "Some antivirus tools include firewalls and email protection too.",
                "Make sure real-time protection is enabled.",
                "Use a reputable antivirus program with good reviews."
            }},
            { "firewall", new List<string> {
                "A firewall blocks unwanted access to your system.",
                "Keep your firewall turned on, especially on public networks.",
                "Firewalls can stop hackers before they even get in.",
                "Configure firewall rules to only allow necessary connections.",
                "Use both hardware and software firewalls for maximum protection."
            }},
            { "encryption", new List<string> {
                "Encryption turns your data into unreadable code without the right key.",
                "Always use encrypted websites (HTTPS).",
                "Apps like WhatsApp use end-to-end encryption to protect your chats.",
                "Encrypt sensitive files before storing or sending them.",
                "Use full-disk encryption on your devices."
            }},
            { "two-factor", new List<string> {
                "Two-factor authentication adds an extra layer of security.",
                "Even if someone gets your password, they'll need your second device.",
                "Enable 2FA on your email and banking apps first.",
                "Use an authentication app instead of SMS for better security.",
                "Never share your 2FA codes with anyone."
            }},
            { "updates", new List<string> {
                "Always install updates to patch security flaws.",
                "Hackers often exploit outdated software.",
                "Turn on automatic updates for convenience and safety.",
                "Update not just your OS, but apps and plugins too.",
                "Delayed updates can leave you vulnerable to zero-day attacks."
            }},
            { "spam", new List<string> {
                "Spam emails often carry scams or malware.",
                "Don't reply or click unsubscribe on suspicious emails.",
                "Use a spam filter and mark junk messages appropriately.",
                "Be cautious with unexpected attachments.",
                "Don't open emails from unknown senders."
            }},
            { "social engineering", new List<string> {
                "Social engineering tricks people, not computers.",
                "Never share passwords, even if someone sounds official.",
                "Scammers often pretend to be from tech support or banks.",
                "Always verify requests through official contact channels.",
                "Trust your instincts — if something feels off, it probably is."
            }}
        };

        public ChatBot(string name)
        {
            userName = name;
            InitializeQuizQuestions();
            ActivityLog.Add($"Session started for {userName} at {DateTime.Now}");
        }
        
        public string ProcessInput(string input)
        {
            input = input.ToLower();
            string response = "";

            if (ContainsAny(input, "bye", "goodbye", "thank you", "thanks", "exit"))
            {
                isRunning = false;
                return $"It was great chatting with you, {userName}. Stay safe and cyber smart! 🌐💬";
            }

            if (string.IsNullOrEmpty(userMood))
            {
                userMood = input;
                response += RespondToMood(userMood) + "\n";
                ActivityLog.Add($"User mood set: {userMood}");
                return response;
            }

            if (!string.IsNullOrEmpty(currentTopic) && ContainsAny(input, "what do you mean", "can you explain", "explain further", "tell me more", "explain more", "i’m confused", "im confused", "i dont understand", "don't understand", "still confused", "in depth", "i don't get it"))
            {
                response += $"Sure! Let me explain more about {currentTopic}:\n" + RespondWithRandomTip(currentTopic);
                ActivityLog.Add($"Clarification requested for {currentTopic}");
                return response;
            }

            if (input.Contains("what am i interested in"))
            {
                return !string.IsNullOrEmpty(favouriteTopic) ?
                    $"You're interested in {favouriteTopic}. Here's a tip:\n{RespondWithRandomTip(favouriteTopic)}" :
                    "You haven't told me what you're interested in yet.";
            }

            // ✅ If user is following up to set a reminder
            if (pendingTask != null)
            {
                if (input.Contains("no"))
                {
                    ActivityLog.Add($"⏳ No reminder set for task '{pendingTask.Title}'");
                    var taskTitle = pendingTask.Title;
                    pendingTask = null;
                    return $"Okay, no reminder set for '{taskTitle}'.";
                }

                DateTime? reminder = null;
                if (input.Contains("1 day")) reminder = DateTime.Now.AddDays(1);
                else if (input.Contains("3 days")) reminder = DateTime.Now.AddDays(3);
                else if (input.Contains("a week") || input.Contains("7 days")) reminder = DateTime.Now.AddDays(7);
                else if (input.Contains("2 weeks") || input.Contains("14 days")) reminder = DateTime.Now.AddDays(14);
                else if (input.Contains("tomorrow")) reminder = DateTime.Now.AddDays(1);

                if (reminder.HasValue)
                {
                    pendingTask.ReminderDate = reminder;
                    ActivityLog.Add($"⏰ Reminder set for '{pendingTask.Title}' on {reminder.Value:dd MMM yyyy}");
                    var taskTitle = pendingTask.Title;
                    pendingTask = null;
                    return $"Reminder set for '{taskTitle}' on {reminder.Value:dd MMM yyyy}.";
                }

                return "When would you like to be reminded? You can say 'in 3 days', 'in 2 weeks', or 'tomorrow'.";
            }

            // 👉 Custom Task Handling via NLP
            if (ContainsAny(input, "task", "remind", "todo", "reminder"))
            {
                string title = input.TrimEnd('.');

                DateTime? reminder = null;
                if (input.Contains("in 1 day")) reminder = DateTime.Now.AddDays(1);
                else if (input.Contains("in 3 days")) reminder = DateTime.Now.AddDays(3);
                else if (input.Contains("in 7 days") || input.Contains("in a week")) reminder = DateTime.Now.AddDays(7);
                else if (input.Contains("in 2 weeks") || input.Contains("in fourteen days")) reminder = DateTime.Now.AddDays(14);

                var task = new CybersecurityTask { Title = ToTitleCase(title), IsCompleted = false, ReminderDate = reminder };
                Tasks.Add(task);

                string log = $"📝 Task added: '{task.Title}'";
                if (reminder.HasValue)
                {
                    log += $" (Reminder set for {reminder.Value:dd MMM yyyy})";
                }
                ActivityLog.Add(log);

                if (!reminder.HasValue)
                {
                    pendingTask = task;
                    return $"Task added: '{task.Title}'. Would you like to set a reminder for this task?";
                }

                return $"Got it! I’ve added the task: '{task.Title}'" + (reminder.HasValue ? $" with reminder for {reminder.Value:dd MMM yyyy}" : "");
            }

            if (ContainsAny(input, "start quiz", "begin quiz", "take quiz", "cyber quiz"))
            {
                ActivityLog.Add($"🧠 Quiz started at {DateTime.Now:HH:mm}");
                return "Opening cybersecurity quiz...";
            }

            if (ContainsAny(input, "show log", "activity log", "what have you done"))
                return GetActivityLogSummary();

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword) || keyword.Split().All(word => input.Contains(word)))
                {
                    currentTopic = keyword;
                    ActivityLog.Add($"Topic accessed: {keyword}");

                    if (string.IsNullOrEmpty(favouriteTopic) && ContainsAny(input, "interested in", "i like", "my favourite", "i’m into", "i am into"))
                    {
                        favouriteTopic = keyword;
                        response += $"Great! I'll remember that you're into {keyword}.\n";
                    }

                    response += RespondToSentiment(input, keyword) + "\n";
                    response += RespondWithRandomTip(keyword);
                    return response;
                }
            }

            return "Hmm, I didn't quite get that. Try asking about phishing, passwords, scams, or another cybersecurity topic.";
        }


        private void InitializeQuizQuestions()
        {
            QuizQuestions.AddRange(QuizQuestion.GetDefaultQuestions());
        }

        private string RespondToMood(string mood)
        {
            if (mood.Contains("worried") || mood.Contains("anxious") || mood.Contains("nervous"))
                return "Cyber threats can be scary, but I'm here to help! 💪 You're not alone.";

            if (mood.Contains("curious") || mood.Contains("interested") || mood.Contains("inquisitive"))
                return "Let's explore cybersecurity together! 🔍 There's so much to discover.";

            if (mood.Contains("frustrated") || mood.Contains("confused") || mood.Contains("overwhelmed"))
                return "I’ll make cybersecurity simple for you! 😊 One step at a time.";

            if (mood.Contains("excited") || mood.Contains("motivated") || mood.Contains("ready"))
                return "Love the energy! 💥 Let’s dive into some cybersecurity tips!";

            if (mood.Contains("bored") || mood.Contains("meh") || mood.Contains("tired"))
                return "Let me share something interesting about cybersecurity to spice things up! ⚡";

            if (mood.Contains("confident") || mood.Contains("good") || mood.Contains("fine"))
                return "Great to hear that! Let’s build on your confidence with more tips. 🛡️";

            if (mood.Contains("scared") || mood.Contains("afraid"))
                return "It’s okay to be scared. I’m here to help you feel more secure. 🕊️";

            return "Thanks for sharing. Let's talk about cybersecurity!";
        }


        private string RespondToSentiment(string input, string topic)
        {
            if (input.Contains("worried") || input.Contains("anxious") || input.Contains("nervous"))
                return $"I understand your concern about {topic}. You're not alone — let's tackle it together.";

            if (input.Contains("curious") || input.Contains("interested") || input.Contains("inquisitive"))
                return $"{ToTitleCase(topic)} is a fascinating topic! Here's something you might not know:";

            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("overwhelmed"))
                return $"Don't worry! I’ll break down {topic} in a simpler way for you.";

            if (input.Contains("excited") || input.Contains("motivated") || input.Contains("eager"))
                return $"Awesome energy! 🔥 Let’s explore more about {topic}.";

            if (input.Contains("bored") || input.Contains("meh") || input.Contains("tired"))
                return $"Let me make {topic} a bit more exciting for you. Stick with me! ⚡";

            if (input.Contains("confident") || input.Contains("fine") || input.Contains("good"))
                return $"Great! Since you’re feeling confident, here’s something deeper about {topic}.";

            if (input.Contains("scared") || input.Contains("afraid"))
                return $"Feeling scared about {topic} is okay. Let’s look at it in a safe, simple way. 🛡️";

            return $"Here's something useful about {topic}.";
        }


        private string RespondWithRandomTip(string topic)
        {
            return keywordResponses.TryGetValue(topic, out var tips)
                ? tips[new Random().Next(tips.Count)]
                : "I don't have information on that topic.";
        }

        private string GetActivityLogSummary()
        {
            return "Recent activity:\n" + string.Join("\n", ActivityLog.TakeLast(5));
        }

        private bool ContainsAny(string input, params string[] phrases)
        {
            return phrases.Any(p => input.Contains(p));
        }

        private string ToTitleCase(string text)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
    }
}
