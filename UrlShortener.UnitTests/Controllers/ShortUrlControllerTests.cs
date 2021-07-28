using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;
using System.Linq;

namespace UrlShortener.UnitTests.Controllers
{
    class ShortUrlControllerTests
    {
        private ShortUrlController _shortUrlController;

        [SetUp]
        public void Setup()
        {
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
        public void Shorten_shouldReturnOkResult()
        {
            // Act
            var result = _shortUrlController.Shorten();

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
