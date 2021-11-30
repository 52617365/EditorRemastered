using System;
using System.Collections.Generic;
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

    internal static class GetDomainList
    {
        internal static IEnumerable<string> Domains() =>
            new string[]
            {
                "123.com",
                "126.com",
                "gmail.com",
                "gmx.at",
                "gmx.com",
                "gmx.de",
                "gmx.fr",
                "gmx.li",
                "gmx.net",
                "hotmail.be",
                "hotmail.co.il",
                "hotmail.co.uk",
                "hotmail.com",
                "hotmail.com.ar",
                "hotmail.com.br",
                "hotmail.com.mx",
                "hotmail.de",
                "hotmail.es",
                "hotmail.fr",
                "hotmail.it",
                "hotmail.kg",
                "hotmail.kz",
                "hotmail.ru",
                "live.be",
                "live.ca",
                "live.co.uk",
                "live.com",
                "live.com.ar",
                "live.com.au",
                "live.com.mx",
                "live.de",
                "live.fr",
                "live.it",
                "live.nl",
                "optusnet.com.au",
                "orange.fr",
                "orange.net",
                "prontomail.com",
                "protonmail.com",
                "qq.com",
                "rocketmail.com",
                "seznam.cz",
                "sky.com",
                "skynet.be",
                "talkcity.com",
                "talktalk.co.uk",
                "usa.com",
                "usa.net",
                "web.de",
                "webmail.co.yu",
                "webmail.co.za",
                "webmail.hu",
                "webmails.com",
                "yahoo.ca",
                "yahoo.co.id",
                "yahoo.co.in",
                "yahoo.co.jp",
                "yahoo.co.kr",
                "yahoo.co.nz",
                "yahoo.co.uk",
                "yahoo.com",
                "yahoo.com.ar",
                "yahoo.com.au",
                "yahoo.com.br",
                "yahoo.com.cn",
                "yahoo.com.hk",
                "yahoo.com.is",
                "yahoo.com.mx",
                "yahoo.com.ph",
                "yahoo.com.ru",
                "yahoo.com.sg",
                "yahoo.de",
                "yahoo.dk",
                "yahoo.es",
                "yahoo.fr",
                "yahoo.ie",
                "yahoo.in",
                "yahoo.it",
                "yahoo.jp",
                "yahoo.ru",
                "yahoo.se",
                "yandex.com",
                "yandex.ru",
            };
    }
}
