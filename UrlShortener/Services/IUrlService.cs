namespace UrlShortener.Services
{
    public interface IUrlService
    {
        string GetShortUrl(string url);
        string DecodeShortUrl(string shortUrl);
    }
}