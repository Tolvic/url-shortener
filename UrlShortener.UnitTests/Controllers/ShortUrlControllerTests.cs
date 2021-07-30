using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;
using System.Linq;
using UrlShortener.Models;
using Moq;
using UrlShortener.Validators;
using UrlShortener.ModelBuilder;

namespace UrlShortener.UnitTests.Controllers
{
    class ShortUrlControllerTests
    {
        private UrlToShorten _validUrlToShorten;
        private const string _shortUrl = "abcdefghijk";

        private Mock<IUrlValidator> _mockUrlValidator;
        private Mock<IShortenedUrlBuilder> _mockShortenedUrlBuilder;
        private ShortUrlController _shortUrlController;

        [SetUp]
        public void Setup()
        {
            _validUrlToShorten = new UrlToShorten
            {
                Url = "https://example.com/page1"
            };

            _mockUrlValidator = new Mock<IUrlValidator>();
            _mockShortenedUrlBuilder = new Mock<IShortenedUrlBuilder>();
            _shortUrlController = new ShortUrlController(_mockUrlValidator.Object, _mockShortenedUrlBuilder.Object);
        }

        [Test]
        public void ShouldImplementController()
        {
            typeof(ShortUrlController).Should().BeAssignableTo<Controller>();
        }

        [Test]
        public void Shorten_ShouldBeDecoratedWithHttpPostAttribute()
        {
            var shortenMethod = typeof(ShortUrlController).Methods().Single(x => x.Name == "Shorten");

            shortenMethod.Should().BeDecoratedWith<HttpPostAttribute>();
        }

        [Test]
        public void Shorten_ShouldBeDecoratedWithValidateAntiForgeryToken()
        {
            var shortenMethod = typeof(ShortUrlController).Methods().Single(x => x.Name == "Shorten");

            shortenMethod.Should().BeDecoratedWith<ValidateAntiForgeryTokenAttribute>();
        }

        [Test]
        public void Shorten_WhenModleStateIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            _shortUrlController.ModelState.AddModelError("error", "error");

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void Shorten_WhenModleStateIsvalid_ShouldCallUrlValidatorIsUrl()
        {
            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            _mockUrlValidator.Verify(x => x.IsUrl(_validUrlToShorten.Url), Times.Once);
        }

        [Test]
        public void Shorten_WhenUrlValidatorReturnsFalse_ShouldReturnBadRequest()
        {
            // Arrange
            _mockUrlValidator.Setup(x => x.IsUrl(_validUrlToShorten.Url)).Returns(false);

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void Shorten_WhenUrlIsValid_ShouldCallShortenedUrlBuilder()
        {
            // Arrange
            SetupHappyPathMocking();

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            _mockShortenedUrlBuilder.Verify(x => x.Build(It.Is<UrlToShorten>(x => x.Url == _validUrlToShorten.Url)), Times.Once);
        }

        [Test]
        public void Shorten_whenPassedValidUrlToShorten_ShouldReturnOkResult()
        {
            // Arrange
            SetupHappyPathMocking();

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public void Shorten_WhenUrlIsValid_ShouldReuturnValueFromUrlBuilder()
        {
            // Arrange
            SetupHappyPathMocking();

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten) as OkObjectResult;

            // Assert
            result.Value.Should().Be(_shortUrl);
        }

        private void SetupHappyPathMocking()
        {
            var shortededUrl = new ShortenedUrl
            {
                Url = _validUrlToShorten.Url,
                ShortUrl = _shortUrl
            };

            _mockUrlValidator.Setup(x => x.IsUrl(_validUrlToShorten.Url)).Returns(true);
            _mockShortenedUrlBuilder.Setup(x => x.Build(It.Is<UrlToShorten>(x => x.Url == _validUrlToShorten.Url))).Returns(shortededUrl);
        }
    }
}
