using System;
using System.IO;
using Spectre.Console;

namespace Project
{
    internal static class FilePath
    {
        private static bool ValidateFilePath(string? path)
        {
            if (path == null)
            {
                return false;
            }

            var pathCheck = new FileInfo(path);
            return pathCheck.Exists;
        }

        internal static string GetFilePath()
        {
            var filePath = AnsiConsole.Ask<string>("Drag combos into terminal");
            return ValidateFilePath(filePath)
                ? filePath
                : throw new ArgumentNullException($"File path does not exist.");
        }
    }

    internal static class EditorMode
    {
        internal static int? GetMode()
        {
            string mode = AnsiConsole.Prompt(
                                             new SelectionPrompt<string>()
                                                 .Title("Select mode:")
                                                 .PageSize(21)
                                                 .AddChoices("Quit", "AppendToMailDomain", "AppendToPassword",
                                                             "DeleteNumbers",
                                                             "DeleteSpecialCharactersFromPass", "DeleteEmptyLines",
                                                             "DomainSwap", "EmailToUsername",
                                                             "ExtractXLength", "GmailEdit",
                                                             "LowerCaseFirstLetterPassword", "LowerCasePassword",
                                                             "SwapNumbers", "SwapPassCaseFirstLetter",
                                                             "SwapPasswordNumbersToUser",
                                                             "SwapUserNumbersToPassword", "UpperCasePassword",
                                                             "UpperCasePasswordFirstLetter",
                                                             "UsernameToEmail", "Sort", "DeleteDuplicates"));
            return ConvertModeToInt(mode);
        }

        private static int? ConvertModeToInt(string mode) =>
            mode switch
            {
                "AppendToMailDomain"              => 0,
                "AppendToPassword"                => 1,
                "DeleteNumbers"                   => 2,
                "DeleteSpecialCharactersFromPass" => 3,
                "DeleteEmptyLines"                => 4,
                "DomainSwap"                      => 5,
                "EmailToUsername"                 => 6,
                "ExtractXLength"                  => 7,
                "GmailEdit"                       => 8,
                "LowerCaseFirstLetterPassword"    => 9,
                "LowerCasePassword"               => 10,
                "SwapNumbers"                     => 11,
                "SwapPassCaseFirstLetter"         => 12,
                "SwapPasswordNumbersToUser"       => 13,
                "SwapUserNumbersToPassword"       => 14,
                "UpperCasePassword"               => 15,
                "UpperCasePasswordFirstLetter"    => 16,
                "UsernameToEmail"                 => 17,
                "Sort"                            => 18,
                "DeleteDuplicates"                => 19,
                _                                 => null,
            };
    }
}
