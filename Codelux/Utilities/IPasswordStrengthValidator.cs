namespace Codelux.Utilities
{
    public interface IPasswordStrengthValidator
    {
        PasswordScore ValidateStrength(string password);
    }
}
