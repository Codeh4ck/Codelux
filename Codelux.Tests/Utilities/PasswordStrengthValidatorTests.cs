using Codelux.Utilities;
using NUnit.Framework;

namespace Codelux.Tests.Utilities
{
    [TestFixture]
    public class PasswordStrengthValidatorTests
    {
        private IPasswordStrengthValidator _strengthValidator;

        [SetUp]
        public void Setup()
        {
            _strengthValidator = new PasswordStrengthValidator();
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndBlankPasswordWhenIValidateBlankScoreIsReturned()
        {
            string password = "";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.Blank, result);
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndVeryWeakPasswordWhenIValidateVeryWeakScoreIsReturned()
        {
            string password = "1234";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.VeryWeak, result);
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndWeakPasswordWhenIValidateWeakScoreIsReturned()
        {
            string password = "1234567";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.Weak, result);
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndMediumPasswordWhenIValidateMediumScoreIsReturned()
        {
            string password = "123456784";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.Medium, result);
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndMediumWithCharsPasswordWhenIValidateMediumScoreIsReturned()
        {
            string password = "123456asdasd";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.Medium, result);
        }

        [Test]
        public void GivenPasswordStrengthValidatorAndStrongWithCharsPasswordWhenIValidateStrongScoreIsReturned()
        {
            string password = "1234wefwefaAFG";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.Strong, result);
        }


        [Test]
        public void GivenPasswordStrengthValidatorAndVeryStrongWithCharsPasswordWhenIValidateVeryStrongScoreIsReturned()
        {
            string password = "1234wefwefaAFG%";
            PasswordScore result = _strengthValidator.ValidateStrength(password);

            Assert.AreEqual(PasswordScore.VeryStrong, result);
        }
    }
}
