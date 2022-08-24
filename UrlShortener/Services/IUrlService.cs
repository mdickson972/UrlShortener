namespace UrlShortener.Services
{
    public interface IUrlService
    {
        string DecodeShortUrl(string shortUrl);
        string GenerateShortUrl(string url);
    }
}