using System;

namespace Project
{
    public class Edit
    {
        private int?   _mode     = Start.GetMode()     ?? throw new ArgumentNullException();
        private string _filePath = Start.GetFilePath() ?? throw new ArgumentNullException();

        private static void EditorEntry()
        {
            // Call edit
        }
    }
}
