using System;
using System.IO;
using Spectre.Console;

namespace Project
{
    internal static class Start
    {
        internal static void StartingScreen() => Ascii();


        #region Ascii

        private static void Ascii()
        {
            Console.Clear();
            string[] author =
            {
                @"   __ _                         _              __ ____ ____ ______ ",
                @"  / _| |                       | |            /_ |___ \___ \____  |",
                @" | |_| | ___  _ __  _ __   __ _| |__  _ __ ___ | | __) |__) |  / / ",
                @" |  _| |/ _ \| '_ \| '_ \ / _` | '_ \| '__/ _ \| ||__ <|__ <  / /  ",
                @" | | | | (_) | |_) | |_) | (_| | |_) | | | (_) | |___) |__) |/ /   ",
                @" |_| |_|\___/| .__/| .__/ \__,_|_.__/|_|  \___/|_|____/____//_/    ",
                @"             | |   | |                                             ",
                @"             |_|   |_|                 							 ",
            };

            string[] program =
            {
                @"  _____ ___ _     _____   _____ ____ ___ _____ ___  ____  ",
                @" |  ___|_ _| |   | ____| | ____|  _ \_ _|_   _/ _ \|  _ \ ",
                @" | |_   | || |   |  _|   |  _| | | | | |  | || | | | |_) |",
                @" |  _|  | || |___| |___  | |___| |_| | |  | || |_| |  _ < ",
                @" |_|   |___|_____|_____| |_____|____/___| |_| \___/|_| \_\",
                @"                                                          ",
            };

            foreach (string line in author)
            {
                Console.WriteLine(line);
            }

            foreach (string line in program)
            {
                Console.WriteLine(line);
            }

            Console.Write("Press a button to continue");
            Console.ReadLine();
        }

        #endregion


        #region MyRegion

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

        #endregion


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
