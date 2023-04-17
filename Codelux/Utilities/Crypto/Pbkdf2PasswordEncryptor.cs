using System;
using System.Text;
using System.Security.Cryptography;

namespace Codelux.Utilities.Crypto;

public class Pbkdf2PasswordEncryptor : IPasswordEncryptor
{
    public string Encrypt(string password)
    {
        byte[] salt = new byte[128 / 8];
        
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA256);

        byte[] hashedPassword = pbkdf2.GetBytes(256 / 8);
        string base64Salt = Convert.ToBase64String(salt);

        return $"{base64Salt}${Convert.ToBase64String(hashedPassword)}";
    }

    public string Decrypt(string password)
    {
        // Pbkdf2 is irreversible - therefore return the password as-in
        // Could alternatively throw an exception but we want the caller to handle that

        return password;
    }

    public bool Verify(string password, string hash)
    {
        string[] hashParts = hash.Split('$');
        if (hashParts.Length != 2) return false;
        
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Convert.FromBase64String(hashParts[0]);
        
        using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000, HashAlgorithmName.SHA256);

        byte[] hashedPassword = pbkdf2.GetBytes(256 / 8);

        return Convert.ToBase64String(hashedPassword) == hashParts[1];
    }
}