using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataRepository _dataService;

        private readonly int ENCODING_BASE_CODE = new Random().Next(100_000, 99_999_999);

        public UrlRepository(IHttpContextAccessor httpContextAccessor, IDataRepository dataService)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataService = dataService;
        }

        public string GetShortUrl(string url)
        {
            // Retrieves list of mapped urls
            var mappedUrls = _dataService.Get<List<UrlMap>>();

            // If the passed url already exists, its shorturl is simply returned
            if (!mappedUrls.Any(x => x.Url.Equals(url)))
            {
                // Continues to generate new potential shortcodes until a unique one is returned
                var shortCode = string.Empty;
                do { shortCode = GenerateShortCode(); }
                while (mappedUrls.Any(x => x.ShortCode.Equals(shortCode)));

                var shortUrl = GenerateShortUrl(shortCode);

                // Adds new url to list of mapped urls to be persisted
                mappedUrls.Add(new UrlMap { Url = url, ShortCode = shortCode, ShortUrl = shortUrl });

                _dataService.Add(mappedUrls);

                return shortUrl;
            }

            return mappedUrls.FirstOrDefault(x => x.Url.Equals(url)).ShortUrl;
        }

        public string DecodeShortUrl(string shortCode)
        {
            var mappedUrls = _dataService.Get<List<UrlMap>>();

            // Returns url that matches on the passed shortcode
            return mappedUrls.FirstOrDefault(x => x.ShortCode.Equals(shortCode))?.Url;
        }

        /// <summary>
        /// Uses a random integer to generate a code that will be used as the identifier
        /// in the shortened URL.
        /// </summary>
        /// <returns>Idenfifier string</returns>
        private string GenerateShortCode()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ENCODING_BASE_CODE));
        }

        /// <summary>
        /// Uses rootUrl and shorcode to generate shortened URL.
        /// </summary>
        /// <param name="shortCode">Identifier string</param>
        /// <returns>Shortened URL string</returns>
        private string GenerateShortUrl(string shortCode)
        {
            var rootUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            return $"https://{rootUrl}/{shortCode}";
        }
    }
}
