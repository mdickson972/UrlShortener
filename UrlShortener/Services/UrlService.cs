using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataService _dataService;

        private readonly int ENCODING_BASE_CODE = new Random().Next(100_000, 99_999_999);

        public UrlService(IHttpContextAccessor httpContextAccessor, IDataService dataService)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataService = dataService;
        }

        public string GetShortUrl(string url)
        {
            var mappedUrls = _dataService.Get<List<UrlMap>>();

            if (!mappedUrls.Any(x => x.Url.Equals(url)))
            {
                var shortCode = string.Empty;
                do { shortCode = GenerateShortCode(); }
                while (mappedUrls.Any(x => x.ShortCode.Equals(shortCode)));

                var shortUrl = GenerateShortUrl(shortCode);
                mappedUrls.Add(new UrlMap { Url = url, ShortCode = shortCode, ShortUrl = shortUrl });

                _dataService.Add(mappedUrls);

                return shortUrl;
            }

            return mappedUrls.FirstOrDefault(x => x.Url.Equals(url)).ShortUrl;
        }

        public string DecodeShortUrl(string shortCode)
        {
            var mappedUrls = _dataService.Get<List<UrlMap>>();

            return mappedUrls.FirstOrDefault(x => x.ShortCode.Equals(shortCode)).Url;
        }

        private string GenerateShortCode()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ENCODING_BASE_CODE));
        }

        private string GenerateShortUrl(string shortCode)
        {
            var rootUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            return $"https://{rootUrl}/{shortCode}";
        }
    }
}
