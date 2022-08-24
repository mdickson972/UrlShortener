using Newtonsoft.Json;
using System;
using System.IO;

namespace UrlShortener.Services
{
    public class DataService : IDataService
    {
        private readonly string ROOT_DATA_DIRECTORY = Path.Combine(Environment.CurrentDirectory, @"Data\");
        private const string URL_MAP_FILE_NAME = "urlMap.json";

        public DataService()
        {
            // Create data directory if doesnt exist
            CreateDirectory(ROOT_DATA_DIRECTORY);
        }

        public void Add<T>(T content)
        {
            var contentString = JsonConvert.SerializeObject(content);
            WriteFile(ROOT_DATA_DIRECTORY, contentString);
        }

        public T Get<T>()
        {
            var contentString = ReadFile(ROOT_DATA_DIRECTORY);
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        /// <summary>
        /// Creates a new directory for passed path
        /// </summary>
        /// <param name="path">Path to the new directory</param>
        private void CreateDirectory(string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Creates a new file for passed path
        /// </summary>
        /// <param name="path">Path to the new file</param>
        private void WriteFile(string path, string content)
        {
            var filePath = $"{path}/{URL_MAP_FILE_NAME}";

            // Serialize content as json and write to file                
            File.WriteAllText(filePath, content);
        }

        private string ReadFile(string path)
        {
            var filePath = $"{path}/{URL_MAP_FILE_NAME}";

                           
            return File.ReadAllText(filePath);
        }
    }
}
