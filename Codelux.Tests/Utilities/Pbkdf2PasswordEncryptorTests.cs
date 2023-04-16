using System;
using Codelux.Utilities.Crypto;
using NUnit.Framework;

namespace Codelux.Tests.Utilities;

[TestFixture]
public class Pbkdf2PasswordEncryptorTests
{
    private Pbkdf2PasswordEncryptor _encryptor;

    private const string PlainTextPassword = "TestPassword123";
    private const string SaltBase64 = "6MUTe4zv3EHNmEfZcAf0lQ==";
    private const string HashedPasswordBase64 = "VLE8vMP+Yi56M5lDIQq5Wj3A70MdnzJpk9qF6C7f59A=";
    private const string HashedPasswordFullBase64 = $"{SaltBase64}${HashedPasswordBase64}";

    [SetUp]
    public void Setup()
    {
        _encryptor = new();
    }

    [Test]
    public void GivenPbkdf2EncryptorWhenIEncryptAPasswordThenValidPbkdf2HashIsReturned()
    {
        string hashed = _encryptor.Encrypt(PlainTextPassword);
        string[] parts = hashed.Split('$');
        
        Assert.That(parts.Length == 2);
        
        byte[] saltBytes = Convert.FromBase64String(parts[0]);
        Assert.That(saltBytes.Length == 16);
    }
    
    [Test]
    public void GivenPbkdf2EncryptorWhenIEncryptAPasswordTwiceThenValidAndUniqueHashIsReturned()
    {
        string hashedOne = _encryptor.Encrypt(PlainTextPassword);
        string hashedTwo = _encryptor.Encrypt(PlainTextPassword);
        
        Assert.That(hashedOne != hashedTwo);
    }

    [Test]
    public void GivenPbkdf2EncryptorWhenIDecryptThenHashedPasswordIsReturned()
    {
        string result = _encryptor.Decrypt(HashedPasswordFullBase64);
        Assert.AreEqual(HashedPasswordFullBase64, result);
    }
    
    [Test]
    public void GivenPbkdf2EncryptorWhenIVerifyCorrectPasswordThenTrueIsReturned()
    {
        bool isVerified = _encryptor.Verify(PlainTextPassword, HashedPasswordFullBase64);
        Assert.IsTrue(isVerified);
    }
    
    [Test]
    public void GivenPbkdf2EncryptorWhenIVerifyWrongPasswordThenFalseIsReturned()
    {
        bool isVerified = _encryptor.Verify("NotTheSamePassword", HashedPasswordFullBase64);
        Assert.IsFalse(isVerified);
    }
}