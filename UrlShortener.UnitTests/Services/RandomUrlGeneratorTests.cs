using FluentAssertions;
using NUnit.Framework;
using UrlShortener.Services;

namespace UrlShortener.UnitTests.Services
{
    class RandomUrlGeneratorTests
    {
        private RandomUrlGenerator _randomUrlGenerator;

        [SetUp]
        public void Setup()
        {
            _randomUrlGenerator = new RandomUrlGenerator();
        }

        [Test]
        public void Generate_ShouldReturnAStringOfTenCharacters()
        {
            // Act
            var result = _randomUrlGenerator.Generate();

            // Assert
            result.Length.Should().Be(10);
        }

        [Test]
        public void Generate_ShouldReturnUniqueStrings()
        {
            // Act
            var result = _randomUrlGenerator.Generate();
            var resultTwo = _randomUrlGenerator.Generate();

            // Assert
            result.Should().NotBe(resultTwo);
        }

        [Test]
        public void Generate_WhenTwoSSeperateInstancesAreUsed_ShouldReturnUniqueStrings()
        {
            // Arrange
            var randomUrlGeneratorTwo = new RandomUrlGenerator();

            // Act
            var result = _randomUrlGenerator.Generate();
            var resultTwo = randomUrlGeneratorTwo.Generate();

            // Assert
            result.Should().NotBe(resultTwo);
        }
    }
}
