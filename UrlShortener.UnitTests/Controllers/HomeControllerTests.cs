using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;
using Moq;
using UrlShortener.Repository;
using UrlShortener.Models;
using System.Linq;

namespace UrlShortener.UnitTests.Controllers
{
    class HomeControllerTests
    {
        public const string _testDataShortUrl = "abcdefghi";
        private Mock<IShortenUrlRepository> _MockShortenedUrlRepository;
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            _MockShortenedUrlRepository = new Mock<IShortenUrlRepository>();
            _homeController = new HomeController(_MockShortenedUrlRepository.Object);
        }

        [Test]
        public void ShouldImplementController()
        {
            typeof(HomeController).Should().BeAssignableTo<Controller>();
        }

        [Test]
        public void Index_returns_ViewResult()
        {
            // Act
            var result = _homeController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void RedirectShortUrlToRealUrl_ShoudlBeDecoratedWithRouteAttribute()
        {
            // Act
            var redirectShortUrlToRealUrlMethod = typeof(HomeController).Methods().Single(x => x.Name == "RedirectShortUrlToRealUrl");

            // Assert
            redirectShortUrlToRealUrlMethod.Should().BeDecoratedWith<RouteAttribute>(attr => attr.Template == "/{shortUrl}");
        }

        [Test]
        public void RedirectShortUrlToRealUrl_MakesCallToShortenUrlReporsitory()
        {
            // Act
            var result = _homeController.RedirectShortUrlToRealUrl(_testDataShortUrl);

            // Assert
            _MockShortenedUrlRepository.Verify(x => x.GetByShortUrl(_testDataShortUrl), Times.Once);
        }

        [Test]
        public void RedirectShortUrlToRealUrl_RedirectsToAction_WhenShortenedUrlRepositoryReturnsNull()
        {
            // Arrange
            _MockShortenedUrlRepository.Setup(x => x.GetByShortUrl(_testDataShortUrl)).Returns((ShortenedUrl)null);

            // Act
            var result = _homeController.RedirectShortUrlToRealUrl(_testDataShortUrl);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Test]
        public void RedirectShortUrlToRealUrl_RedirectsToIndex_WhenShortenedUrlRepositoryReturnsNull()
        {
            // Arrange
            _MockShortenedUrlRepository.Setup(x => x.GetByShortUrl(_testDataShortUrl)).Returns((ShortenedUrl)null);

            // Act
            var result = _homeController.RedirectShortUrlToRealUrl(_testDataShortUrl) as RedirectToActionResult;

            // Assert
            result.ActionName.Should().Be("Index");
        }

        [Test]
        public void RedirectShortUrlToRealUrl_Redirects_WhenShortenedUrlRepositoryReturnsValue()
        {
            // Arrange
            _MockShortenedUrlRepository.Setup(x => x.GetByShortUrl(_testDataShortUrl)).Returns(new ShortenedUrl { Url = "https://google.com"});

            // Act
            var result = _homeController.RedirectShortUrlToRealUrl(_testDataShortUrl);

            // Assert
            result.Should().BeOfType<RedirectResult>();
        }

        [Test]
        public void RedirectShortUrlToRealUrl_RedirectsToValue_WhenShortenedUrlRepositoryReturnsAValue()
        {
            // Arrange
            var longUrl = "https://google.com";
            _MockShortenedUrlRepository.Setup(x => x.GetByShortUrl(_testDataShortUrl)).Returns(new ShortenedUrl { Url = longUrl });

            // Act
            var result = _homeController.RedirectShortUrlToRealUrl(_testDataShortUrl) as RedirectResult;

            // Assert
            result.Url.Should().Be(longUrl);
        }
    }
}
