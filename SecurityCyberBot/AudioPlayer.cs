using System;
using System.IO;
using System.Media;
using System.Windows;

namespace SecurityCyberBot.Utilities
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            try
            {
                string fileName = "ChatBotVoice.wav";
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (File.Exists(filePath))
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.PlaySync();
                    }
                }
                else
                {
                    MessageBox.Show($"Audio file not found: {filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing audio: {ex.Message}", "Audio Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
