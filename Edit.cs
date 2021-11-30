using System;
using System.Collections.Generic;

namespace Project
{
    public class Edit
    {
        private                 int? _mode = EditorMode.GetMode() ?? throw new ArgumentNullException();
        private                 string _filePath = FilePath.GetFilePath() ?? throw new ArgumentNullException();
        private static readonly IEnumerable<string> DomainList = GetDomainList.Domains();


        private static void EditorEntry()
        {
            // Call edit
        }
    }
}
