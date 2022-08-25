namespace UrlShortener.Repositories
{
    public interface IUrlRepository
    {
        /// <summary>
        /// Shortens given URL into a more managably lengthed link.
        /// </summary>
        /// <param name="url">Complete url to be shortened</param>
        /// <returns>Shortened Url String</returns>
        string GetShortUrl(string url);

        /// <summary>
        /// Expands shortened URL to return the originally passed link.
        /// </summary>
        /// <param name="shortCode">Identifier value that is passed as query string parameter in shortened URL</param>
        /// <returns>Original URL that was passed</returns>
        string DecodeShortUrl(string shortCode);
    }
}