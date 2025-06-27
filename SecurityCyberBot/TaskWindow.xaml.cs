using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCyberBot
{
    public partial class TaskWindow : Window
    {
        public List<CybersecurityTask> Tasks { get; set; }
        private ChatBot chatbot; // NEW: Reference to chatbot for logging

        public TaskWindow(ChatBot bot)
        {
            InitializeComponent();
            chatbot = bot;
            Tasks = chatbot.Tasks;
            dgTasks.ItemsSource = Tasks;

            cmbReminderTime.SelectionChanged += ReminderTime_Changed;

            // ✅ Remember which tasks were already completed
            alreadyCompleted = new HashSet<string>(
                Tasks.Where(t => t.IsCompleted)
                     .Select(t => t.Title));
        }

        private void ReminderTime_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (cmbReminderTime.SelectedItem is ComboBoxItem selected && selected.Content.ToString() == "Custom...")
            {
                dtpCustomDate.Visibility = Visibility.Visible;
                dtpCustomDate.SelectedDate = DateTime.Now.AddDays(1);
            }
            else
            {
                dtpCustomDate.Visibility = Visibility.Collapsed;
            }
        }
        private HashSet<string> alreadyCompleted = new HashSet<string>(); // NEW

        private void TaskCompleted_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox)?.DataContext is CybersecurityTask task)
            {
                // Only log if it wasn't already completed before
                if (task.IsCompleted && !alreadyCompleted.Contains(task.Title))
                {
                    chatbot.ActivityLog.Add($"✅ Task marked as completed: '{task.Title}'");
                    alreadyCompleted.Add(task.Title); // Avoid logging again
                }
            }
        }




        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new CybersecurityTask
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                IsCompleted = false
            };

            if (chkReminder.IsChecked == true)
            {
                newTask.ReminderDate = cmbReminderTime.SelectedIndex switch
                {
                    0 => DateTime.Now.AddDays(1),
                    1 => DateTime.Now.AddDays(3),
                    2 => DateTime.Now.AddDays(7),
                    3 => DateTime.Now.AddDays(14),
                    4 => dtpCustomDate.SelectedDate,
                    _ => null
                };
            }

            Tasks.Add(newTask);
            chatbot.ActivityLog.Add($"📝 Task added: '{newTask.Title}'{(newTask.ReminderDate.HasValue ? $" (Reminder set for {newTask.ReminderDate.Value:dd MMM yyyy})" : "")}");
            RefreshTaskList();
            ClearInputs();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is CybersecurityTask task)
            {
                Tasks.Remove(task);
                chatbot.ActivityLog.Add($"🗑️ Task deleted: '{task.Title}'");
                RefreshTaskList();
            }
        }

        private void RefreshTaskList()
        {
            dgTasks.Items.Refresh();
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            chkReminder.IsChecked = false;
            cmbReminderTime.SelectedIndex = -1;
            dtpCustomDate.Visibility = Visibility.Collapsed;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
