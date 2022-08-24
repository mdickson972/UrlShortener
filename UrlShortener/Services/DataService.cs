using Newtonsoft.Json;
using System;
using System.IO;

namespace UrlShortener.Services
{
    public class DataService : IDataService
    {
        private string filePath;

        public DataService()
        {
            var rootDataDirectory = Path.Combine(Environment.CurrentDirectory, @"Data\");
            var urlMapFileName = "urlMap.json";

            filePath = $"{rootDataDirectory}/{urlMapFileName}";
        }

        public void Add<T>(T content)
        {
            var contentString = JsonConvert.SerializeObject(content);
            WriteFile(contentString);
        }

        public T Get<T>()
        {
            var contentString = ReadFile();
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        /// <summary>
        /// Creates a new file for passed path
        /// </summary>
        /// <param name="path">Path to the new file</param>
        private void WriteFile(string content)
        {
            File.WriteAllText(filePath, content);
        }

        private string ReadFile()
        {
            return File.ReadAllText(filePath);
        }
    }
}
