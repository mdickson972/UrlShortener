
namespace UrlShortener.Models.Shortener
{
    public class Url
    {
        public string BaseUrl { get; set; }
        public string ShortenedUrl { 
            get => ShortenUrl(BaseUrl); 
        }

        public Url() { }

        private string ShortenUrl(string url)
        {
            return string.Empty;
        }

    }
}
