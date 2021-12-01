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

                for (var i = 0; i < line.Length; i++)
                {
                    if (!char.IsSymbol(line[i]) && !char.IsPunctuation(line[i]))
                    {
                        sb.Append(line[i]);
                    }
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

            static string Edit(string line)
            {
                if (!IsEmail(line)) return line;

                // Line split
                string?[]? userPass = line.SplitByChar(':');

                // Mail split
                string?[]? ms = userPass?[0].SplitByChar('@');

                return new string($"{ms![0]}{userPass![1]}");
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

            string Edit(string line)
            {
                if (!IsEmail(line)) return line;

                string?[]? userPass = line.SplitByChar(':');
                string?[]? ms       = userPass?[0].SplitByChar('@');

                return new string($"{ms![0]}{append}{ms[1]}:{userPass![1]}");
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
                string?[]? userPass = line.SplitByChar(':');

                char[] a = userPass![1]!.ToCharArray();
                a[0] = char.IsUpper(a[0]) ? a[0] = char.ToUpper(a[0]) : char.ToLower(a[0]);
                return new string($"{userPass[0]}:{a}");
            }
        }

        internal static string? SwapPassNumbersToUser(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line)
            {
                string?[]? userPass = line.SplitByChar(':');
                // If password does not contain a number return.
                if (!HasNumbers(userPass![1]!)) return line;

                string numbers = GetNumbers(userPass[1]!);


                if (!IsEmail(userPass[0]!)) return new string($"{userPass[0]}{numbers}:{userPass[1]}");

                // If email username.
                string[] ms = userPass[0].SplitByChar('@')!;
                return new string($"{ms[0]}{numbers}{ms[1]}:{userPass[1]}");
            }
        }

        internal static string? SwapUserNumbersToPass(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string Edit(string line)
            {
                // If line has no numbers, return.
                if (!HasNumbers(line)) return line;

                string?[]? userPass = line.SplitByChar(':');


                return new
                    string($"{GetLetters(userPass![0]!)}:{userPass[1]}{GetNumbers(userPass[0]!)}");
            }
        }

        internal static string? ExtractXFromPass(this string? str, int length)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            string Edit(string line)
            {
                string?[]? splitCombo = line.SplitByChar(':');
                return new string($"{splitCombo![0]}:{splitCombo[1]![..length]}");
            }
        }

        internal static string? SwapNumbers(this string? str)
        {
            return CheckFormat(str) ? Edit(str!) : null;

            static string? Edit(string line)
            {
                if (!HasNumbers(line)) return line;

                return IsEmail(line) ? MailSwap(line) : UserSwap(line);
            }

            static string? MailSwap(string? line)
            {
                string?[]? splitByColon = line.SplitByChar(':');
                string?[]? splitEmail   = splitByColon?[0].SplitByChar('@');
                // Just swapping numbers from the mail into the password and vica verca.
                var swappedCombo
                    = $"{GetLetters(splitEmail![0]!)}{GetNumbers(splitByColon![1]!)}{GetLetters(splitEmail[1]!)}:{GetLetters(splitByColon[1]!)}{GetNumbers(splitByColon[0]!)}";
                return swappedCombo;
            }

            static string? UserSwap(string? line)
            {
                string?[]? splitByColon = line.SplitByChar(':');
                if (splitByColon is null) return null;
                // Just swapping numbers from the user into the password and vica verca.
                var swappedCombo
                    = $"{GetLetters(splitByColon[0]!)}{GetNumbers(splitByColon[1]!)}:{GetLetters(splitByColon[1]!)}{GetNumbers(splitByColon[0]!)}";
                return swappedCombo;
            }
        }

        private static string GetLetters(string line)
        {
            StringBuilder sb = new(line.Length);
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsLetter(line[i]))
                {
                    sb.Append(line[i]);
                }
            }

            return sb.ToString();
        }


        private static string GetNumbers(string line)
        {
            StringBuilder sb = new(line.Length);
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    sb.Append(line[i]);
                }
            }

            return sb.ToString();
        }

        private static bool HasNumbers(string line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
