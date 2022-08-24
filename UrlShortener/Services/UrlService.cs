using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private const int ENCODING_BASE_CODE = 123456789;

        public string GenerateShortUrl(string url)
        {
            var requestedUrl = "http://www.google.com";

            var chunk = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ENCODING_BASE_CODE));

            return string.Empty;
        }

        public string DecodeShortUrl(string shortUrl)
        {
            return string.Empty;
        }
    }
}
