using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models.ViewModels;

namespace UrlShortener.Controllers
{
    [Route("/")]
    public class ShortenerController : Controller
    {
        private readonly ILogger<ShortenerController> _logger;

        public ShortenerController(ILogger<ShortenerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ShortenerViewModel());
        }


        [HttpPost]
        public IActionResult Index(ShortenerViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
