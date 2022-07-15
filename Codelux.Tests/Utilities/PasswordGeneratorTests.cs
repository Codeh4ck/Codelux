using System.Linq;
using NUnit.Framework;
using Codelux.Utilities;

namespace Codelux.Tests.Utilities
{
    [TestFixture]
    public class PasswordGeneratorTests
    {
        private PasswordGenerator _passwordGenerator;

        [SetUp]
        public void Setup()
        {
            _passwordGenerator = new PasswordGenerator();
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordsThenPasswordsAreNotIdentical()
        {
            int length = 10;

            string firstPassword = _passwordGenerator.GeneratePassword(length);
            string secondPassword = _passwordGenerator.GeneratePassword(length);

            Assert.IsNotEmpty(firstPassword);
            Assert.IsNotEmpty(secondPassword);

            Assert.IsFalse(firstPassword == secondPassword);
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGenerateFixedLengthPasswordThenLengthIsHonored()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length);

            Assert.IsNotEmpty(password);
            Assert.AreEqual(length, password.Length);
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithCapitalsThenCapitalsAreIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, true);

            Assert.IsNotEmpty(password);
            Assert.IsTrue(password.Any(char.IsUpper));
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithoutCapitalsThenCapitalsAreNotIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, false);

            Assert.IsNotEmpty(password);
            Assert.IsFalse(password.Any(char.IsUpper));
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithNumbersThenNumbersAreIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, false, true, false);

            Assert.IsNotEmpty(password);
            Assert.IsTrue(password.Any(char.IsNumber));
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithoutNumbersThenNumbersAreNotIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, false, false, false);

            Assert.IsNotEmpty(password);
            Assert.IsFalse(password.Any(char.IsNumber));
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithSymbolsThenSymbolsAreIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, false, false, true);

            char[] symbols = "!@#$%^&*()_+-=[]{};:,./<>?".ToCharArray();

            Assert.IsNotEmpty(password);
            Assert.IsTrue(password.IndexOfAny(symbols) > -1);
        }

        [Test]
        public void GivenPasswordGeneratorWhenIGeneratePasswordWithoutSymbolsThenSymbolsAreNotIncluded()
        {
            int length = 10;
            string password = _passwordGenerator.GeneratePassword(length, false, false, false);

            char[] symbols = "!@#$%^&*()_+-=[]{};:,./<>?".ToCharArray();

            Assert.IsNotEmpty(password);
            Assert.IsTrue(password.IndexOfAny(symbols) == -1);
        }
    }
}
