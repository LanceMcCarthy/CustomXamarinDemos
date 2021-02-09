using System;
using System.Collections.ObjectModel;
using System.IO;
using CallDetector.Portable.Models;
using Newtonsoft.Json;

namespace CallDetector.Portable.Helpers
{
    public static class FileHelpers
    {
        private static string _localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Iterates over all files that match the search pattern.
        /// Search Pattern examples: "*.*" will return all files types, "*.png" will return all png image files, "*.log" will return all log files
        /// </summary>
        /// <returns>An array of complete file paths.</returns>
        public static string[] GetLocalFolderFilePaths(string searchPattern)
        {
            // 2 - Get a list of files in that folder using the pattern
            var filePaths = Directory.GetFiles(_localFolder, "*.*", SearchOption.TopDirectoryOnly);

            return filePaths;
        }
        

        public static void SaveToCache(this ObservableCollection<Caller> calls)
        {
            var json = JsonConvert.SerializeObject(calls);
            var filePath = Path.Combine(_localFolder, "calls.json");

            File.WriteAllText(filePath, json);
        }

        public static void LoadFromCache(this ObservableCollection<Caller> calls)
        {
            var filePath = Path.Combine(_localFolder, "calls.json");

            var serializedCalls = File.ReadAllText(filePath);

            var deserializedCalls = JsonConvert.DeserializeObject<ObservableCollection<Caller>>(serializedCalls);

            calls.Clear();

            foreach (var caller in deserializedCalls)
            {
                calls.Add(caller);
            }
        }
    }
}
