using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SecurityCyberBot
    {
        public class CybersecurityTask
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime? ReminderDate { get; set; }

            public string ReminderText => ReminderDate.HasValue
                ? $"Reminder on {ReminderDate.Value:MM/dd/yyyy}"
                : "No reminder";

            // Static task management methods
            public static string HandleTaskCommand(string input, List<CybersecurityTask> tasks, List<string> activityLog)
            {
                if (ContainsAny(input, "add task", "create task", "new task", "remind me to"))
                    return AddTask(input, tasks, activityLog);

                if (ContainsAny(input, "show tasks", "view tasks", "my tasks"))
                    return ListTasks(tasks);

                if (ContainsAny(input, "complete task", "finish task"))
                    return CompleteTask(input, tasks, activityLog);

                return "I didn't understand that task command.";
            }

            private static string AddTask(string input, List<CybersecurityTask> tasks, List<string> activityLog)
            {
                string taskTitle = ExtractAfter(input, "add task", "create task", "new task", "remind me to");

                var newTask = new CybersecurityTask
                {
                    Title = taskTitle,
                    Description = $"Security task: {taskTitle}",
                    ReminderDate = ExtractReminderDate(input)
                };

                tasks.Add(newTask);
                activityLog.Add($"Added task: {taskTitle}");

                return $"Task added: '{taskTitle}'" +
                       (newTask.ReminderDate.HasValue
                           ? $" with reminder on {newTask.ReminderDate.Value:MM/dd/yyyy}"
                           : "");
            }

            private static string ListTasks(List<CybersecurityTask> tasks)
            {
                if (tasks.Count == 0) return "You have no active tasks.";

                return "Your tasks:\n" + string.Join("\n",
                    tasks.Select((t, i) => $"{i + 1}. {t.Title} - {t.ReminderText}")) +
                    "\n\nType 'complete task X' or 'delete task X' to manage.";
            }

            private static string CompleteTask(string input, List<CybersecurityTask> tasks, List<string> activityLog)
            {
                int taskIndex = ExtractNumber(input) - 1;
                if (taskIndex >= 0 && taskIndex < tasks.Count)
                {
                    tasks[taskIndex].IsCompleted = true;
                    activityLog.Add($"Completed task: {tasks[taskIndex].Title}");
                    return $"Marked task '{tasks[taskIndex].Title}' as completed!";
                }
                return "Invalid task number.";
            }

            // Helper methods
            private static bool ContainsAny(string input, params string[] phrases)
                => phrases.Any(input.Contains);

            private static string ExtractAfter(string input, params string[] phrases)
            {
                foreach (var phrase in phrases.Where(input.Contains))
                {
                    return input.Substring(input.IndexOf(phrase) + phrase.Length).Trim();
                }
                return input;
            }

            private static DateTime? ExtractReminderDate(string input)
            {
                if (input.Contains("tomorrow")) return DateTime.Now.AddDays(1);
                if (input.Contains("next week")) return DateTime.Now.AddDays(7);
                if (input.Contains("in 3 days")) return DateTime.Now.AddDays(3);
                return null;
            }

            private static int ExtractNumber(string input)
            {
                var match = System.Text.RegularExpressions.Regex.Match(input, @"\d+");
                return match.Success ? int.Parse(match.Value) : -1;
            }
        }
    }
