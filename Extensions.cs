using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    public static class Extensions
    {
        private static bool CheckFormat(string? line) => !string.IsNullOrEmpty(line) && line.Contains(':');

        private static bool IsEmail(string? line) => line.Contains('@');

        private static string[]? SplitByChar(this string? str, char character)
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
                string[]? lp = line.SplitByChar(':');

                return $"{lp}{domain}:{lp![1]}";
            }
        }


        private static string? EmailsToUsername(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string? line)
            {
                if (!IsEmail(line)) return line;

                // Line split
                string[]? lp = line.SplitByChar(':');

                // Mail split
                string[]? ms = lp?[0].SplitByChar('@');

                if (lp is null || ms is null) return null;

                // Length of first index without an email + delimiter + the second index length.
                StringBuilder sb = new(ms[0].Length + 1 + lp[1].Length);

                sb.AppendFormat(ms[0] + lp[1]);

                return sb.ToString();
            }
        }

        internal static string? AppendToEnd(this string? str, string append)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string Edit(string line)
            {
                return $"{line}{append}";
            }
        }
    }
}
