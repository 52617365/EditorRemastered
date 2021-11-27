using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    public static class Extensions
    {
        private static bool IsEmail(string line) => line.Contains(':') && line.Contains('@');

        private static IReadOnlyList<string>? SplitByChar(this string str, char character) => str.Split(character);


        private static string RemoveSpecialChar(this string str)
        {
            StringBuilder sb = new();
            foreach (char c in str.Where(static c =>
                                             c is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '.' or
                                                 '_'))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        private static string RemoveNumbers(this string str)
        {
            StringBuilder sb = new();
            foreach (char c in str.Where(static c => !char.IsDigit(c)))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        private static string RemoveLetters(this string str)
        {
            StringBuilder sb = new();
            foreach (char c in str.Where(c => !str.Contains(c)))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
