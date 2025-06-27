using System;
using System.Collections.Generic;

namespace SecurityCyberBot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }

        // Static method to provide default questions
        public static List<QuizQuestion> GetDefaultQuestions()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> {
                        "Reply with your password",
                        "Delete the email",
                        "Report the email as phishing",
                        "Ignore it"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Never share passwords via email. Legitimate companies will never ask this way."
                },
                new QuizQuestion
                {
                    Question = "Which of these is the strongest password?",
                    Options = new List<string> {
                        "password123",
                        "JohnDoe1985",
                        "P@ssw0rd!",
                        "Tr0ub4dor&3"
                    },
                    CorrectAnswerIndex = 3,
                    Explanation = "Correct! The strongest passwords are long and include numbers, symbols, and mixed cases."
                },
                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> {
                        "A long password",
                        "Using two passwords for the same account",
                        "Verification using something you know and something you have",
                        "A backup email address"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! 2FA uses both a password and a second factor like a phone or app."
                },
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    Options = new List<string> {
                        "A method of password creation",
                        "A type of firewall",
                        "A scam where attackers trick users into revealing personal information",
                        "An antivirus technique"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Phishing is a social engineering attack used to steal data."
                },
                new QuizQuestion
                {
                    Question = "Why should you avoid public Wi-Fi for sensitive tasks?",
                    Options = new List<string> {
                        "It's too slow",
                        "It uses up your battery",
                        "It can expose your data to hackers",
                        "It limits data usage"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Public Wi-Fi is often unsecured, making it easy for hackers to intercept your data."
                },
                new QuizQuestion
                {
                    Question = "What does a firewall do?",
                    Options = new List<string> {
                        "Increases computer speed",
                        "Physically protects your computer",
                        "Blocks unauthorized access to your network",
                        "Cools down your hardware"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Firewalls act as barriers to block unwanted traffic."
                },
                new QuizQuestion
                {
                    Question = "Why are software updates important?",
                    Options = new List<string> {
                        "They make apps look cooler",
                        "They add more ads to your apps",
                        "They fix bugs and patch security vulnerabilities",
                        "They delete your old files"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Updates keep your software secure and functioning properly."
                },
                new QuizQuestion
                {
                    Question = "What is malware?",
                    Options = new List<string> {
                        "A new coding language",
                        "Software designed to protect your data",
                        "Malicious software that harms or exploits systems",
                        "A social media app"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Malware is software intended to cause damage or steal data."
                },
                new QuizQuestion
                {
                    Question = "What should you do before clicking on a suspicious-looking link?",
                    Options = new List<string> {
                        "Click it and find out",
                        "Forward it to your friends",
                        "Hover over it to preview the URL",
                        "Reply asking for more info"
                    },
                    CorrectAnswerIndex = 2,
                    Explanation = "Correct! Hovering lets you see where the link really leads."
                },
                new QuizQuestion
                {
                    Question = "What is encryption used for?",
                    Options = new List<string> {
                        "Storing passwords in a document",
                        "Making text unreadable to unauthorized users",
                        "Boosting Wi-Fi signals",
                        "Updating apps automatically"
                    },
                    CorrectAnswerIndex = 1,
                    Explanation = "Correct! Encryption scrambles data so only authorized users can read it."
                }
            };
        }
    }
}
