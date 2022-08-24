using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private int ENCODING_BASE_CODE = new Random().Next(100_000, 99_999_999);
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        

        public string GetShortUrl(string url)
        {
            return GenerateShortUrl(url);
        }

        public string DecodeShortUrl(string shortUrl)
        {
            return string.Empty;
        }

        private string GenerateShortUrl(string url)
        {
            var rootUrl = _httpContextAccessor.HttpContext.Request.Host.Value;

            var shortCode = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ENCODING_BASE_CODE));

            return $"{rootUrl}/{shortCode}";
        }
    }
}
