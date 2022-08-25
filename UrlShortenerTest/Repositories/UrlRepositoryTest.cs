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
            var orgUrl = "www.test.com";
            var expectedShortCode = "abcdef";
            var expectedShortUrl = $"{ROOT_URL}/abcdef";

            var urlMap = new List<UrlMap>() { new UrlMap { Url = orgUrl, ShortCode = expectedShortCode, ShortUrl = expectedShortUrl } };

            mockDataRepository.Setup(s => s.Get<List<UrlMap>>()).Returns(urlMap);

            // Act
            var actual = urlRepository.GetShortUrl(orgUrl);

            // Assert
            Assert.Equal(expectedShortUrl, actual);
        }
    }
}
