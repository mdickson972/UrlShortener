using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataService _dataService;
        private readonly int ENCODING_BASE_CODE = new Random().Next(100_000, 99_999_999);

        private List<UrlMap> _urlMaps;

        public UrlService(IHttpContextAccessor httpContextAccessor, IDataService dataService)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataService = dataService;

            _urlMaps = _dataService.Get<List<UrlMap>>();
            if(_urlMaps == null)
            {
                _dataService.Add(new List<UrlMap>());
            }
        }

        public string GetShortUrl(string url)
        {
            if (!_urlMaps.Exists(x => x.Url.Equals(url)))
            {
                var shortCode = GenerateShortCode();
                var shortUrl = GenerateShortUrl(shortCode);
                _urlMaps.Add(
                    new UrlMap
                    {
                        Url = url,
                        ShortCode = shortCode,
                        ShortUrl = shortUrl
                    });

                _dataService.Add(_urlMaps);

                return shortUrl;
            }

            return _urlMaps.Find(x => x.Url.Equals(url)).ShortUrl;
        }

        public string DecodeShortUrl(string shortCode)
        {
            return _urlMaps.Find(x => x.ShortCode.Equals(shortCode)).Url;
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
