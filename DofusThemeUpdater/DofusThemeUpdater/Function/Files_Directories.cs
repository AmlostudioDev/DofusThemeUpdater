using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DofusThemeUpdater
{
    public partial class Function
    {
        private static List<string> GetDirectories(string path, string searchPattern = "*",
        SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }
        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }

        private static List<string> GetFiles(string path, string searchPattern = "*")
        {
            return Directory.GetFiles(path, searchPattern).ToList();
        }
    }
}
