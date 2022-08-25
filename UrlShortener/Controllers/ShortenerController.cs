using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models.ViewModels;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IUrlService _urlService;
        private readonly ILogger<ShortenerController> _logger;

        public ShortenerController(IUrlService urlService, ILogger<ShortenerController> logger)
        {
            _urlService = urlService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ShortenerViewModel());
        }


        [HttpPost]
        public IActionResult Index(ShortenerViewModel vm)
        {
            vm.ShortenedUrl = _urlService.GetShortUrl(vm.Url);
            return View(vm);
        }

        [HttpGet, Route("{shortCode}")]
        public IActionResult Index(string shortCode)
        {
            var url = _urlService.DecodeShortUrl(shortCode);
            return Redirect(url);
        }
    }
}
