﻿using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.ViewModels;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class ShortenerController : Controller
    {
        private readonly IUrlService _urlService;

        public ShortenerController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        public IActionResult Index() => View(new ShortenerViewModel());

        [HttpPost]
        public IActionResult Index(ShortenerViewModel vm)
        {
            vm.ShortenedUrl = _urlService.GetShortUrl(vm.Url);
            return View(vm);
        }

        [HttpGet, Route("{shortCode}")]
        public IActionResult Redirection(string shortCode)
        {
            var url = _urlService.DecodeShortUrl(shortCode);

            // If shortened url is not valid, redirect to home page.
            if (string.IsNullOrEmpty(url)) { return RedirectToAction("Index", "Shortener"); }
                        
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult Error() => View();
    }
}
