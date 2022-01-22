using System.Security.Cryptography;
using System.Text;

namespace Codelux.Utilities.Crypto
{
    public class Md5PasswordEncryptor : IPasswordEncryptor
    {
        public string Encrypt(string password)
        {
            StringBuilder passwordBuilder = new(32);
            MD5 md5Provider = MD5.Create();
            UTF8Encoding encoding = new();

            byte[] hashBytes = md5Provider.ComputeHash(encoding.GetBytes(password));

            foreach (byte b in hashBytes)
                passwordBuilder.Append(b.ToString("X2"));

            return passwordBuilder.ToString().ToLower();
        }

        public string Decrypt(string password)
        {
            // MD5 is irreversible - therefore return the password as-in
            // Could alternatively throw an exception but we want the caller to handle that
            return password;
        }
    }
}
