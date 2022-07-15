namespace Codelux.Utilities
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(int length, bool capitals = true, bool numbers = false, bool symbols = false);
    }
}
