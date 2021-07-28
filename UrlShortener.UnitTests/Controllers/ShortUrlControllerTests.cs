using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.UnitTests.Controllers
{
    class ShortUrlControllerTests
    {
        private UrlToShorten _validUrlToShorten;
        private ShortUrlController _shortUrlController;

        [SetUp]
        public void Setup()
        {
            _validUrlToShorten = new UrlToShorten
            {
                Url = "https://example.com/page1"
            };

            _shortUrlController = new ShortUrlController();
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
        public void Shorten_whenMoleStateIsInvalid_shouldReturnBadRequest()
        {
            // Arrange
            _shortUrlController.ModelState.AddModelError("error", "error");

            // Act
            var result = _shortUrlController.Shorten(_validUrlToShorten);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
