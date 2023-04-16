using System;
using System.Text;

namespace Codelux.Utilities.Crypto
{
    public class Base64Encryptor : IPasswordEncryptor
    {
        public string Encrypt(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(data);
        }

        public string Decrypt(string password)
        {
            byte[] data = Convert.FromBase64String(password);
            return Encoding.UTF8.GetString(data);
        }

        public bool Verify(string password, string hash) => Encrypt(password) == hash;
    }
}
