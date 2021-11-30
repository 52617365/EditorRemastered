#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace Project
{
    public static class Extensions
    {
        private static bool CheckFormat(string? line) => !string.IsNullOrEmpty(line) && line.Contains(':');

        private static bool IsEmail(string line)
        {
            if (line is null) throw new ArgumentNullException(nameof(line));
            return line.Contains('@');
        }

        private static string?[]? SplitByChar(this string? str, char character)
        {
            try
            {
                return str?.Split(character);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static string? RemoveSpecialChars(this string str)
        {
            return CheckFormat(str) ? Edit(str) : null;

            static string Edit(string line)
            {
                StringBuilder sb = new(line.Length);
                foreach (char ch in line.Where(static c =>
                                                   !char.IsSymbol(c)
                                                   &&
                                                   !char.IsPunctuation(c)))
                {
                    sb.Append(ch);
                }

                return sb.ToString();
            }
        }

        internal static string? RemoveNumbers(this string str)
        {
            return CheckFormat(str) ? Edit(str) : null;

            static string Edit(string line)
            {
                StringBuilder sb = new(line.Length);
                foreach (char c in line.Where(static c => !char.IsDigit(c)))
                {
                    sb.Append(c);
                }

                return sb.ToString();
            }
        }

        internal static string? RemoveLetters(this string str)
        {
            return CheckFormat(str) ? Edit(str) : null;

            static string Edit(string line)
            {
                StringBuilder sb = new(line.Length);
                foreach (char c in line.Where(static c => !char.IsLetter(c)))
                {
                    sb.Append(c);
                }

                return sb.ToString();
            }
        }

        internal static string? UsernamesToEmail(this string? str, string domain)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string Edit(string line)
            {
                if (IsEmail(line)) return line;

                // Line split
                string?[]? userPass = line.SplitByChar(':');

                return new string($"{userPass}{domain}:{userPass![1]}");
            }
        }

        private static string? MailToUsername(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                if (!IsEmail(line)) return line;

                // Line split
                string?[]? userPass = line.SplitByChar(':');

                // Mail split
                string?[]? ms = userPass?[0].SplitByChar('@');

                if (userPass is null || ms is null) return null;

                return new string($"{ms[0]}{userPass[1]}");
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

                string?[]? userPass = line.SplitByChar(':');
                string?[]? ms       = userPass?[0].SplitByChar('@');

                if (userPass is null || ms is null) return null;

                return new string($"{ms[0]}{append}{ms[1]}:{userPass[1]}");
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

            static string? Edit(string line)
            {
                string?[]? userPass = line.SplitByChar(':');
                if (userPass is null) return null;

                char[] a = userPass[1]!.ToCharArray();
                a[0] = char.IsUpper(a[0]) ? a[0] = char.ToUpper(a[0]) : char.ToLower(a[0]);
                return new string($"{userPass[0]}:{a}");
            }
        }

        internal static string? SwapPassNumbersToUser(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                string?[]? userPass = line.SplitByChar(':');
                if (userPass is null) return null;
                // If password does not contain a number return.
                if (userPass[1]!.Any(static x => !char.IsDigit(x))) return line;

                StringBuilder sb = new(userPass[1]!.Length);
                foreach (char number in userPass[1]!.Where(char.IsDigit))
                {
                    sb.Append(number);
                }

                if (!IsEmail(userPass[0]!)) return new string($"{userPass[0]}{sb}:{userPass[1]}");

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
                string?[]? userPass = line.SplitByChar(':');
                if (userPass is null) return null;

                // If username does not contain a number return.
                if (userPass[0]!.Any(static x => !char.IsDigit(x))) return line;

                StringBuilder sb = new(userPass[0]!.Length);
                foreach (char number in userPass[0]!.Where(char.IsDigit))
                {
                    sb.Append(number);
                }

                return new string($"{userPass[0]}:{userPass[1]}{sb}");
            }
        }

        internal static string? ExtractXFromPass(this string? str, int length)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string Edit(string line)
            {
                string?[]? splitCombo    = line.SplitByChar(':');
                string     extractedPass = Extract(splitCombo![1]).ToString();
                return new string($"{splitCombo[0]}:{extractedPass}");
            }

            ReadOnlySpan<char> Extract(string? line)
            {
                ReadOnlySpan<char> extractedString = line.AsSpan()[length..];
                return extractedString;
            }
        }

        internal static string? SwapNumbers(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                if (line.Any(static x => !char.IsDigit(x))) return line;

                return IsEmail(line) ? MailSwap(line) : UserSwap(line);
            }

            static string? MailSwap(string? line)
            {
                string?[]? splitByColon = line.SplitByChar(':');
                string?[]? splitEmail   = splitByColon?[0].SplitByChar('@');
                if (splitByColon is null || splitEmail == null) return null;
                // Just swapping numbers from the mail into the password and vica verca.
                // I realise it could be faster (LINQ is slow and there is a lot of allocations) AND easier to read.
                var swappedCombo
                    = $"{GetLetters(splitEmail[0]!)}{GetNumbers(splitByColon[1]!)}{GetLetters(splitEmail[1]!)}:{GetLetters(splitByColon[1]!)}{GetNumbers(splitByColon[0]!)}";
                return swappedCombo;
            }

            static string? UserSwap(string? line)
            {
                string?[]? splitByColon = line.SplitByChar(':');
                if (splitByColon is null) return null;
                // Just swapping numbers from the user into the password and vica verca.
                // I realise it could be faster (LINQ is slow and there is a lot of allocations) AND easier to read.
                var swappedCombo
                    = $"{GetLetters(splitByColon[0]!)}{GetNumbers(splitByColon[1]!)}:{GetLetters(splitByColon[1]!)}{GetNumbers(splitByColon[0]!)}";
                return swappedCombo;
            }

            static string GetLetters(string line)
            {
                StringBuilder sb = new();
                foreach (char c in line.Where(static x => !char.IsDigit(x)))
                {
                    sb.Append(c);
                }

                return sb.ToString();
            }


            static IEnumerable<char> GetNumbers(string line)
            {
                StringBuilder sb = new();
                foreach (char c in line.Where(char.IsDigit))
                {
                    sb.Append(c);
                }

                return sb.ToString();
            }
        }
    }
}
