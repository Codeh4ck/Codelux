namespace Codelux.Utilities.Crypto
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
        string Decrypt(string password);
        bool Verify(string password, string hash);
    }
}
