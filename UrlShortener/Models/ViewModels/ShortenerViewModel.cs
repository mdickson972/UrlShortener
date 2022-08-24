using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models.ViewModels
{
    public class ShortenerViewModel
    {
        [Required(ErrorMessage = "Please enter a valid URL to be shortened")]
        [Url(ErrorMessage = "The provided string is not a valid URL")]
        public string Url { get; set; }
        public string ShortenedUrl { get => Url; set => ShortenedUrl = value; }        
    }
}
