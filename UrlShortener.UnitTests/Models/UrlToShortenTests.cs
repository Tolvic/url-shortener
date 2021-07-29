using FluentAssertions;
using NUnit.Framework;
using UrlShortener.Models;

namespace UrlShortener.UnitTests.Models
{
    class UrlToShortenTests
    {
        [Test]
        public void ShouldEncodeUrlToBeShortened()
        {
            // Arrange
            var maliciousInput = "https://test.com/<script>alert('Injected!');</script>";
            var expectedUutputUrl = "https://test.com/&lt;script&gt;alert(&#39;Injected!&#39;);&lt;/script&gt;";

            // Act
            var urlToBeShortened = new UrlToShorten
            {
                Url = maliciousInput
            };

            // Assert
            urlToBeShortened.Url.Should().Be(expectedUutputUrl);
        }
    }
}
