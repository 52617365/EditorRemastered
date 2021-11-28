using System;
using System.Linq;
using System.Text;

namespace Project
{
    public static class Extensions
    {
        private static bool CheckFormat(string? line) => !string.IsNullOrEmpty(line) && line.Contains(':');

        private static bool IsEmail(string line) => line.Contains('@');

        private static string[]? SplitByChar(this string str, char character)
        {
            try
            {
                return str.Split(character);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static string RemoveSpecialChars(this string str)
        {
            StringBuilder sb = new(str.Length);
            foreach (char ch in str.Where(static c =>
                                              !char.IsSymbol(c)
                                              &&
                                              !char.IsPunctuation(c)))
            {
                sb.Append(ch);
            }

            return sb.ToString();
        }

        internal static string RemoveNumbers(this string str)
        {
            StringBuilder sb = new(str.Length);
            foreach (char c in str.Where(static c => !char.IsDigit(c)))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        internal static string RemoveLetters(this string str)
        {
            StringBuilder sb = new(str.Length);
            foreach (char c in str.Where(static c => !char.IsLetter(c)))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        internal static string? UsernamesToEmail(this string? str, string domain)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string Edit(string line)
            {
                if (IsEmail(line)) return line;

                // Line split
                string[]? userPass = line.SplitByChar(':');

                return $"{userPass}{domain}:{userPass![1]}";
            }
        }

        private static string? UserPassToUsername(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                if (!IsEmail(line)) return line;

                // Line split
                string[]? userPass = line.SplitByChar(':');

                // Mail split
                string[]? ms = userPass?[0].SplitByChar('@');

                if (userPass is null || ms is null) return null;

                return $"{ms[0]}{userPass[1]}";
            }
        }

        internal static string? AppendToEnd(this string? str, string append)
        {
            return CheckFormat(str) ? Edit(str!) : null;
            string Edit(string line) => $"{line}{append}";
        }

        internal static string? MailUserAppend(this string? str, string append)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string? Edit(string line)
            {
                if (!IsEmail(line)) return line;

                string[]? userPass = line.SplitByChar(':');
                string[]? ms       = userPass?[0].SplitByChar('@');

                if (userPass is null || ms is null) return null;

                return $"{ms[0]}{append}{ms[1]}:{userPass[1]}";
            }
        }

        internal static string? ToLowerCase(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line) => line.ToLower();
        }

        internal static string? ToUpperCase(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line) => line.ToUpper();
        }

        internal static string? SwapPassCaseFirstLetter(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line)
            {
                string[]? userPass = line.SplitByChar(':');
                char[]    a        = userPass![1].ToCharArray();
                a[0] = char.IsUpper(a[0]) ? a[0] = char.ToUpper(a[0]) : char.ToLower(a[0]);
                return new string($"{userPass[0]}:{a}");
            }
        }

        internal static string? SwapPassNumbersToUser(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line)
            {
                string[]? userPass = line.SplitByChar(':');
                // If password does not contain a number return.
                if (userPass![1].Any(static x => !char.IsDigit(x))) return line;

                StringBuilder sb = new(userPass[1].Length);
                foreach (char number in userPass[1].Where(char.IsDigit))
                {
                    sb.Append(number);
                }

                if (!IsEmail(userPass[0])) return new string($"{userPass[0]}{sb}:{userPass[1]}");

                // If email username.
                string[] ms = userPass[0].SplitByChar('@')!;
                return new string($"{ms[0]}{sb}{ms[1]}:{userPass[1]}");
            }
        }

        internal static string? SwapUserNumbersToPass(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                string[]? userPass = line.SplitByChar(':');
                // If username does not contain a number return.
                if (userPass![0].Any(static x => !char.IsDigit(x))) return line;

                StringBuilder sb = new(userPass[0].Length);
                foreach (char number in userPass[0].Where(char.IsDigit))
                {
                    sb.Append(number);
                }

                return new string($"{userPass[0]}:{userPass[1]}{sb}");
            }
        }
    }
}
