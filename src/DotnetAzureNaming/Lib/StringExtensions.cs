using System.Text.RegularExpressions;

internal static class StringExtensions
{
    internal static string replace(this string str, string pattern, string replacement)
    {
        return Regex.Replace(str, pattern, replacement);
    }

    internal static bool test(this string pattern, string str)
    {
        return Regex.IsMatch(str, pattern);
    }
}
