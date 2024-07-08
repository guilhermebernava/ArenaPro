using System.Text.RegularExpressions;

namespace ArenaPro.Domain.Utils;
public static class EmailUtils
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
        {
            return false;
        }

        return EmailRegex.IsMatch(email);
    }
}