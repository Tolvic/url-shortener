using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShortener.Controllers;
using FluentAssertions;

namespace UrlShortener.UnitTests.Controllers
{
    class HomeControllerTests
    {
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            _homeController = new HomeController();
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
    }
}
