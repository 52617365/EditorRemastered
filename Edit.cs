using System;

namespace Project
{
    public class Edit
    {
        private int?   _mode     = EditorMode.GetMode()     ?? throw new ArgumentNullException();
        private string _filePath = FilePath.GetFilePath() ?? throw new ArgumentNullException();

        private static void EditorEntry()
        {
            // Call edit
        }
    }
}
