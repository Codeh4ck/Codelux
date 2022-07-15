using System;
using System.Text;

namespace Codelux.Utilities
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public string GeneratePassword(int length, bool capitals = true, bool numbers = false, bool symbols = false)
        {
            string initialPool = "abcdefghijklmnopqrstuvwxyz";

            StringBuilder charPoolBuilder = new StringBuilder();

            charPoolBuilder.Append(initialPool);

            if (capitals)
                charPoolBuilder.Append(initialPool.ToUpper());

            if (numbers)
                charPoolBuilder.Append("0123456789");

            if (symbols)
                charPoolBuilder.Append("!@#$%^&*()_+-=[]{};:,./<>?");

            string charPool = charPoolBuilder.ToString();

            StringBuilder passwordBuilder = new StringBuilder();

            for (int x = 0; x < length; x++)
                passwordBuilder.Append(charPool[Random.Next(0, charPool.Length - 1)]);

            return passwordBuilder.ToString();
        }
    }
}
