using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using UrlShortener.Models;
using UrlShortener.Repositories;
using Xunit;

namespace UrlShortenerTest.Repositories
{
    public class UrlRepositoryTest
    {
        private const string ROOT_URL = "http://shortUrl.com";
        private readonly Mock<IHttpContextAccessor> mockhttpContextAccessor = new Mock<IHttpContextAccessor>();
        private readonly Mock<IDataRepository> mockDataRepository = new Mock<IDataRepository>();

        private UrlRepository urlRepository;

        public UrlRepositoryTest()
        {
            urlRepository = new UrlRepository(mockhttpContextAccessor.Object, mockDataRepository.Object);
        }

        [Fact]
        public void GetShortUrl_WhenCallingGetShortUrl_WithPreviouslyPersistedUrl_ShortUrlIsReturned()
        {
            // Arrange
            var urlMap = new UrlMap { Url = "www.test.com", ShortCode = "abcdef", ShortUrl = $"{ROOT_URL}/abcdef" };
            var urlMapList = new List<UrlMap> { urlMap };
            var expected = urlMap.ShortUrl;

            mockDataRepository.Setup(s => s.Get<List<UrlMap>>()).Returns(urlMapList);

            // Act
            var actual = urlRepository.GetShortUrl(urlMap.Url);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DecodeShortUrl_WhenCallingDecodeShortUrl_WithValidShortUrl_FullUrlIsReturned()
        {
            // Arrange
            var urlMap = new UrlMap { Url = "www.test.com", ShortCode = "abcdef", ShortUrl = $"{ROOT_URL}/abcdef" };
            var urlMapList = new List<UrlMap> { urlMap };
            var expected = urlMap.Url;

            mockDataRepository.Setup(s => s.Get<List<UrlMap>>()).Returns(urlMapList);

            // Act
            var actual = urlRepository.DecodeShortUrl(urlMap.ShortCode);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DecodeShortUrl_WhenCallingDecodeShortUrl_WithInvalidShortUrl_NullStringIsReturned()
        {
            // Arrange
            var urlMap = new UrlMap { Url = "www.test.com", ShortCode = "abcdef", ShortUrl = $"{ROOT_URL}/abcdef" };
            var urlMapList = new List<UrlMap> { urlMap };
            var invalidShortCode = "invalid";

            mockDataRepository.Setup(s => s.Get<List<UrlMap>>()).Returns(urlMapList);

            // Act
            var result = urlRepository.DecodeShortUrl(invalidShortCode);

            // Assert
            Assert.Null(result);
        }
    }
}
