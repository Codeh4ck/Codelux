using System.Text.RegularExpressions;

namespace Codelux.Utilities
{
    public class PasswordStrengthValidator : IPasswordStrengthValidator
    {
        public PasswordScore ValidateStrength(string password)
        {
            PasswordScore score = PasswordScore.Blank;

            if (password.Length < 1)
                return PasswordScore.Blank;

            if (password.Length < 5)
                return PasswordScore.VeryWeak;

            if (password.Length >= 5)
                score = PasswordScore.Weak;

            if (password.Length >= 8 && password.Length <= 12)
                score = PasswordScore.Medium;

            if (Regex.Match(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", RegexOptions.ECMAScript).Success)
               score = PasswordScore.Strong;

            if (Regex.Match(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!,@,#,$,%,^,&,*,(,),\[,\],;,',\.,\\,\|,\?,\:,\"",\/,\-,\\_,\=,\+,\<,\>]).{8,}$",
                RegexOptions.None).Success)
                score = PasswordScore.VeryStrong;

            return score;

        }
    }
}
