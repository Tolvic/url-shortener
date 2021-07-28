using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;
using System.Linq;
using UrlShortener.Models;
using Moq;
using UrlShortener.Validators;

namespace UrlShortener.UnitTests.Controllers
{
    class ShortUrlControllerTests
    {
        private UrlToShorten _validUrlToShorten;
        private Mock<IUrlValidator> _mockUrlValidator;
        private ShortUrlController _shortUrlController;

        [SetUp]
        public void Setup()
        {
            _validUrlToShorten = new UrlToShorten
            {
                Url = "https://example.com/page1"
            };

            _mockUrlValidator = new Mock<IUrlValidator>();
            _shortUrlController = new ShortUrlController(_mockUrlValidator.Object);
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
        public void Shorten_whenpassedvalidUrlToShorten_shouldReturnOkResult()
        {
            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void Shorten_whenModleStateIsInvalid_shouldReturnBadRequest()
        {
            // Arrange
            _shortUrlController.ModelState.AddModelError("error", "error");

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void Shorten_whenModleStateIsvalid_shouldCallUrlValidatorIsUrl()
        {
            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            _mockUrlValidator.Verify(x => x.IsUrl(_validUrlToShorten.Url), Times.Once);
        }

        [Test]
        public void Shorten_whenUrlValidatorReturnsFalse_shouldReturnBadRequest()
        {
            // Arrange
            _mockUrlValidator.Setup(x => x.IsUrl(_validUrlToShorten.Url)).Returns(false);

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
