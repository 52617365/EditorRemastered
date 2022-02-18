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
