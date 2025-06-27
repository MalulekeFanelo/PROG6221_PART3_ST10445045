using System.Media;

namespace SecurityCyberBot.Utilities
{
    public class AudioPlayer
    {
        public void PlayGreeting()
        {
            try
            {
                string filePath = @"C:\path\to\ChatBotVoice.wav"; // Update path
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            catch { /* Handle error silently */ }
        }
    }
}

    