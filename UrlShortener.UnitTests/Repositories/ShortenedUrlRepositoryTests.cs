using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using UrlShortener.Models;
using UrlShortener.Repository;

namespace UrlShortener.UnitTests.Repositories
{
    class ShortenedUrlRepositoryTests
    {
        private const string shortUrlTestData = "abcdefghi";
        private const string longUrlTestData = "https://test.com/dsadsadsadsadasd";

        private UrlShortenerContext _context;
        private ShortenedUrlRepository _shortenedUrlRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UrlShortenerContext>()
                .UseInMemoryDatabase("testDb")
                .Options;
            _context = new UrlShortenerContext(options);
            _shortenedUrlRepository = new ShortenedUrlRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void GetByShortUrl_WhenNoResultIsFound_returnsNull()
        {
            // Act
            var result = _shortenedUrlRepository.GetByShortUrl(shortUrlTestData);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void GetByShortUrl_WhenResultIsFound_returnsExpectedResult()
        {
            var expectedResult = GetSeedData();
            _context.ShortenedUrl.Add(expectedResult);
            _context.SaveChanges();

            // Act
            var result = _shortenedUrlRepository.GetByShortUrl(shortUrlTestData);

            // Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void GetByLongUrl_WhenNoResultIsFound_returnsNull()
        {
            // Act
            var result = _shortenedUrlRepository.GetByLongUrl(shortUrlTestData);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void GetByLongUrl_WhenResultIsFound_returnsExpectedResult()
        {
            var expectedResult = GetSeedData();
            _context.ShortenedUrl.Add(expectedResult);
            _context.SaveChanges();

            // Act
            var result = _shortenedUrlRepository.GetByLongUrl(longUrlTestData);

            // Assert
            result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Id));
        }

        [Test]
        public void Add_ShouldUpdatedb()
        {
            var shortenedUrl = GetSeedData();
  
            // Act
            _shortenedUrlRepository.Add(shortenedUrl);

            // Assert
            var dbEntry = _context.ShortenedUrl.Single(x => x.Url == shortenedUrl.Url && x.ShortUrl == shortenedUrl.ShortUrl);

            dbEntry.Should().BeEquivalentTo(shortenedUrl, options => options.Excluding(x => x.Id));
        }

        private ShortenedUrl GetSeedData()
        {
            return new ShortenedUrl
            {
                Url = longUrlTestData,
                ShortUrl = shortUrlTestData
            };
        }
    }
}
