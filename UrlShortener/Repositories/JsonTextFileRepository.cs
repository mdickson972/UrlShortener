using Newtonsoft.Json;
using System;
using System.IO;

namespace UrlShortener.Repositories
{
    public class JsonTextFileRepository : IDataRepository
    {
        private string filePath;

        public JsonTextFileRepository()
        {
            // Generates path to file to be used as data source.
            var rootDataDirectory = Path.Combine(Environment.CurrentDirectory, "Data");
            var urlMapFileName = "urlMap.json";
            filePath = $"{rootDataDirectory}/{urlMapFileName}";

            VerifyDirectoryExists(rootDataDirectory);
            VerifyFileExists(filePath);
        }

        public void Add<T>(T content)
        {
            // Serializes passed data object to json string
            var contentString = JsonConvert.SerializeObject(content);
            WriteFile(contentString);
        }

        public T Get<T>()
        {
            var contentString = ReadFile();

            // If valid content is returned the json string is deserialized into data object;
            // otherwise new instance of the intended data object is returned.
            return !string.IsNullOrEmpty(contentString)
                ? JsonConvert.DeserializeObject<T>(contentString)
                : (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Writes passed content to data source file.
        /// </summary>
        /// <param name="path">Path to the new file</param>
        private void WriteFile(string content)
        {
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// Reads data source file and returns contents.
        /// </summary>
        /// <returns>String representing data held in data source file</returns>
        private string ReadFile()
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Checks to ensure directory for data source file exists; creating it if not.
        /// </summary>
        /// <param name="directoryPath">Location of Data directory</param>
        private void VerifyDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Checks to ensure file to be used as data source exists; creating it if not.
        /// </summary>
        /// <param name="filePath">Path to data source file</param>
        private void VerifyFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }
        }
    }
}
