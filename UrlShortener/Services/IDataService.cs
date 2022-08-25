namespace UrlShortener.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Persist new data
        /// </summary>
        /// <typeparam name="T">Type of the data to be held</typeparam>
        /// <param name="content">Data to be persisted</param>
        void Add<T>(T content);

        /// <summary>
        /// Returns previously persisted data
        /// </summary>
        /// <typeparam name="T">Type of the data held</typeparam>
        /// <returns>Data object from data source</returns>
        T Get<T>();
    }
}