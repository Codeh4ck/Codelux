using Codelux.Utilities.Crypto;
using NUnit.Framework;

namespace Codelux.Tests.Utilities
{
    [TestFixture]
    public class Base64EncryptorTests
    {
        private Base64Encryptor _encryptor;

        private const string TestString = "This is a test string!";
        private const string TestStringBase64 = "VGhpcyBpcyBhIHRlc3Qgc3RyaW5nIQ=="; // Encoded with external Base64 encoding tool for cross-reference

        [SetUp]
        public void Setup()
        {
            _encryptor = new();
        }

        [Test]
        public void GivenBase64EncryptorWhenIEncryptThenValidBase64IsReturned()
        {
            string result = _encryptor.Encrypt(TestString);
            Assert.AreEqual(TestStringBase64, result);
        }

        [Test]
        public void GivenBase64EncryptorWhenIDecryptBase64StringThenCorrectResultIsReturned()
        {
            string result = _encryptor.Encrypt(TestString);
            Assert.AreEqual(TestStringBase64, result);

            result = _encryptor.Decrypt(result);
            Assert.AreEqual(TestString, result);
        }
        
        [Test]
        public void GivenBase64EncryptorWhenIVerifyCorrectPasswordThenTrueIsReturned()
        {
            bool isVerified = _encryptor.Verify(TestString, TestStringBase64);
            Assert.IsTrue(isVerified);
        }
        
        [Test]
        public void GivenBase64EncryptorWhenIVerifyWrongPasswordThenFalseIsReturned()
        {
            bool isVerified = _encryptor.Verify("NotTheSameString", TestStringBase64);
            Assert.IsFalse(isVerified);
        }
    }
}
