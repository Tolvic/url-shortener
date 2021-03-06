using FluentAssertions;
using Moq;
using NUnit.Framework;
using UrlShortener.ModelBuilder;
using UrlShortener.Models;
using UrlShortener.Repository;
using UrlShortener.Services;

namespace UrlShortener.UnitTests.Builders
{
    class ShortenedUrlBuilderTests
    {
        private UrlToShorten _urlToShorten;
        private Mock<IShortenUrlRepository> _MockShortenedUrlrepository;
        private Mock<IRandomUrlGenerator> _MockRandomUrlGenerator;
        private ShortenedUrlBuilder _shortenedUrlBuilder;

        [SetUp]
        public void Setup()
        {
            _urlToShorten = new UrlToShorten
            {
                Url = "https://example.com"
            };
            _MockShortenedUrlrepository = new Mock<IShortenUrlRepository>();
            _MockRandomUrlGenerator = new Mock<IRandomUrlGenerator>();
            _shortenedUrlBuilder = new ShortenedUrlBuilder(_MockShortenedUrlrepository.Object, _MockRandomUrlGenerator.Object);
        }

        [Test]
        public void Build_ShouldCallShortenedUrlRepositoryGetByLongUrl()
        {
            // Act
            _ = _shortenedUrlBuilder.Build(_urlToShorten);

            // Assert
            _MockShortenedUrlrepository.Verify(x => x.GetByLongUrl(_urlToShorten.Url));
        }

        [Test]
        public void Build_WhenShortenedUrlRepositoryGetByLongUrReturnsAValue_ShouldReturnExpectedResult()
        {
            // Arrange
            var expectedResult = new ShortenedUrl
            {
                Url = _urlToShorten.Url,
                ShortUrl = "abcdefghijk"
            };

            _MockShortenedUrlrepository.Setup(x => x.GetByLongUrl(_urlToShorten.Url)).Returns(expectedResult);

            // Act
            var result = _shortenedUrlBuilder.Build(_urlToShorten);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void Build_WhenShortenedUrlRepositoryGetByLongUrlReturnsNull_ShouldCallRandomUrlGeneratorGenerate()
        {
            // Arrange
            _MockShortenedUrlrepository.Setup(x => x.GetByLongUrl(_urlToShorten.Url)).Returns((ShortenedUrl)null);

            // Act
            _ = _shortenedUrlBuilder.Build(_urlToShorten);

            // Assert
            _MockRandomUrlGenerator.Verify(x => x.Generate());
        }

        [Test]
        public void Build_WhenShortenedUrlRepositoryGeByLongUrlReturnsNull_ShouldCallShortenedUrlRepositoryAdd()
        {
            // Arrange
            var shortUrl = "abcdefghijk";

            _MockShortenedUrlrepository.Setup(x => x.GetByLongUrl(_urlToShorten.Url)).Returns((ShortenedUrl)null);
            _MockRandomUrlGenerator.Setup(x => x.Generate()).Returns(shortUrl);

            // Act
            _ = _shortenedUrlBuilder.Build(_urlToShorten);

            // Assert
            _MockShortenedUrlrepository.Verify(x => x.Add(It.Is<ShortenedUrl>(x => x.Url == _urlToShorten.Url && x.ShortUrl == shortUrl)));
        }

        [Test]
        public void Build_WhenShortenedUrlRepositoryGetByLongUrlReturnsNull_ReturnsExpectedResult()
        {
            // Arrange
            var shortUrl = "abcdefghijk";
            var expectedResult = new ShortenedUrl
            {
                Url = _urlToShorten.Url,
                ShortUrl = shortUrl
            };

            _MockShortenedUrlrepository.Setup(x => x.GetByLongUrl(_urlToShorten.Url)).Returns((ShortenedUrl)null);
            _MockRandomUrlGenerator.Setup(x => x.Generate()).Returns(shortUrl);

            // Act
            var result = _shortenedUrlBuilder.Build(_urlToShorten);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
