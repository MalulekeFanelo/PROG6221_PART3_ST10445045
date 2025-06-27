using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace SecurityCyberBot.Utilities
{
    public class ImageDisplay
    {
        public string GetAsciiArt()
        {
            try
            {
                // Use relative path instead of absolute path
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "ChatBotLogo.jpg");

                if (!File.Exists(imagePath))
                {
                    return GetFallbackAsciiArt();
                }

                int width = 60;  // Adjust as needed
                int height = 31; // Adjust as needed

                return ConvertImageToAscii(imagePath, width, height);
            }
            catch
            {
                return GetFallbackAsciiArt();
            }
        }

        private string ConvertImageToAscii(string imagePath, int newWidth, int newHeight)
        {
            var asciiBuilder = new StringBuilder();
            string asciiChars = "@%#*+=-:. "; // ASCII characters ordered by brightness

            using (Bitmap image = new Bitmap(imagePath))
            using (Bitmap resizedImage = new Bitmap(image, new Size(newWidth, newHeight)))
            {
                for (int y = 0; y < resizedImage.Height; y++)
                {
                    for (int x = 0; x < resizedImage.Width; x++)
                    {
                        Color pixelColor = resizedImage.GetPixel(x, y);
                        int grayValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        int index = grayValue * (asciiChars.Length - 1) / 255;
                        asciiBuilder.Append(asciiChars[index]);
                    }
                    asciiBuilder.AppendLine();
                }
            }

            return asciiBuilder.ToString();
        }

        private string GetFallbackAsciiArt()
        {
            return @"
           .--------.
          / .------. \
         / /        \ \
         | |        | |
        _| |________| |_
      .' |_|        |_| '.
     '._____ ____ _____.'  CYBER LOCKED
     |     .'____'.     |  ───────────────
     '.__.'.'    '.'.__.'
     '.__  | LOCK |  __.'
     |   '.'.____.'.'   |    🔒
     '.____'.____.'____.'

     [🔐 Secure. Smart. Cyber-Aware.]
";
        }
    }
}
