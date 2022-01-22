using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Codelux.Utilities
{
    public class BasicCaptchaProvider
    {
        private readonly Random _random = new();
        public string CaptchaText { get; private set; }

        public Bitmap CreateCaptchaImage()
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException(
                    $"{nameof(CreateCaptchaImage)} is only supported on Windows machines.");

            CaptchaText = GetRandomText();

            Bitmap bitmap = new(200, 50, PixelFormat.Format32bppArgb);

            Pen pen = new(Color.Yellow);
            Rectangle rectangle = new(0, 0, 200, 50);

            SolidBrush blackBrush = new(Color.Black);
            SolidBrush whiteBrush = new(Color.White);

            using Graphics graphics = Graphics.FromImage(bitmap);

            int counter = 0;

            graphics.DrawRectangle(pen, rectangle);
            graphics.FillRectangle(blackBrush, rectangle);

            foreach (char character in CaptchaText)
            {
                graphics.DrawString(character.ToString(), new("Georgia", 10 + _random.Next(14, 18)), whiteBrush, new PointF(10 + counter, 10));
                counter += 20;
            }

            DrawRandomLines(graphics);

            return bitmap;

        }

        private void DrawRandomLines(Graphics g)
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException(
                    $"{nameof(CreateCaptchaImage)} is only supported on Windows machines.");

            SolidBrush green = new(Color.Green);

            for (int i = 0; i < 20; i++)
                g.DrawLines(new(green, 2), GetRandomPoints());

        }
        private Point[] GetRandomPoints()
        {
            Point[] points =
            {
                new(_random.Next(10, 150), _random.Next(10, 150)),
                new(_random.Next(10, 100), _random.Next(10, 100))
            };

            return points;
        }

        private static string GetRandomText()
        {
            StringBuilder randomText = new();

            string charPool = "abcdefghijklmnopqrstuvwxyz1234567890";

            Random rand = new();
            for (int j = 0; j <= 5; j++)
                randomText.Append(charPool[rand.Next(charPool.Length)]);

            return randomText.ToString();
        }
    }
}
