using Codelux.Utilities.Crypto;
using NUnit.Framework;

namespace Codelux.Tests.Utilities
{
    [TestFixture]
    public class Md5PasswordEncryptorTests
    {
        private Md5PasswordEncryptor _encryptor;

        private const string PlainTextPassword = "TestPassword123";
        private const string HashedPassword = "9b599faac222a0dfcfab49148ce40c26"; // Hashed with external MD5 hashing tool for cross-reference

        [SetUp]
        public void Setup()
        {
            _encryptor = new();
        }

        [Test]
        public void GivenMd5EncryptorWhenIEncryptAPasswordThenValidMd5HashIsReturned()
        {
            string md5 = _encryptor.Encrypt(PlainTextPassword);
            Assert.AreEqual(HashedPassword, md5);
        }

        [Test]
        public void GivenMd5EncryptorWhenIDecryptThenHashedPasswordIsReturned()
        {
            string result = _encryptor.Decrypt(HashedPassword);
            Assert.AreEqual(HashedPassword, result);
        }

        [Test]
        public void GivenMd5EncryptorWhenIVerifyCorrectPasswordThenTrueIsReturned()
        {
            bool isVerified = _encryptor.Verify(PlainTextPassword, HashedPassword);
            Assert.IsTrue(isVerified);
        }
        
        [Test]
        public void GivenMd5EncryptorWhenIVerifyWrongPasswordThenFalseIsReturned()
        {
            bool isVerified = _encryptor.Verify("NotTheSamePassword", HashedPassword);
            Assert.IsFalse(isVerified);
        }
    }
}
