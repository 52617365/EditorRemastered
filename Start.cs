using System;

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
    }
}
