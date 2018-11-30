using System;
using System.IO;

namespace CallDetector.Portable.Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        /// Iterates over all files that match the search pattern.
        /// Search Pattern examples: "*.*" will return all files types, "*.png" will return all png image files, "*.log" will return all log files
        /// </summary>
        /// <returns>An array of full file paths.</returns>
        public static string[] GetLocalFolderFilePaths(string searchPattern)
        {
            // 1 - Get the LocalFolder path for the native app environment 
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // 2 - Get a list of files in that folder
            var filePaths = Directory.GetFiles(localFolder, "*.*", SearchOption.TopDirectoryOnly);

            return filePaths;
        }

        public static void GenerateSampleLogFiles()
        {
            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            for (var i = 1; i < 14; i++)
            {
                var fileName = $"Call {i}.log";
                var filePath = Path.Combine(localFolder, fileName);
                var fileContents = "This is sample log file text content.";

                File.WriteAllText(filePath, fileContents);
            }
        }
    }
}
