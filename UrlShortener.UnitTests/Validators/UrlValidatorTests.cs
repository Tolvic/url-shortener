using NUnit.Framework;
using FluentAssertions;
using UrlShortener.Validators;

namespace UrlShortener.UnitTests.Validators
{
    class UrlValidatorTests
    {
        private UrlValidator _urlValidator;

        [SetUp]
        public void Setup()
        {
            _urlValidator = new UrlValidator();
        }

        [Test]
        public void ShouldImplementController()
        {
            typeof(UrlValidator).Should().Implement<IUrlValidator>();
        }

        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("test", false)]
        [TestCase("test.com", false)]
        [TestCase("www.test.com", false)]
        [TestCase("http://www.test.com/<", false)]
        [TestCase("http://www.test.com/>", false)]
        [TestCase("http://www.test.com%", false)]
        [TestCase("https://test.com", true)]
        [TestCase("http://test.com", true)]
        [TestCase("https://www.test.com", true)]
        [TestCase("http://www.test.com", true)]
        [TestCase("https://test.com/page1", true)]
        [TestCase("http://test.com/page1", true)]
        [TestCase("https://www.test.com/page1", true)]
        [TestCase("http://www.test.com/page1", true)]
        [TestCase("https://test.com/directory1/page1", true)]
        [TestCase("http://test.com/directory1/page1", true)]
        [TestCase("https://www.test.com//directory1/page1", true)]
        [TestCase("http://www.test.com/directory1/page1", true)]
        [TestCase("https://subdomain1.subdomain2.test.com", true)]
        [TestCase("http://subdomain1.subdomain2.test.com", true)]
        [TestCase("https://www.test.com/directory1/page1?color=blue", true)]
        [TestCase("http://www.test.com/directory1/page1?color=blue", true)]
        

        public void ShouldValidUrl(string urlToValidate, bool expectedResult)
        {
            // act
            var result = _urlValidator.IsUrl(urlToValidate);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
