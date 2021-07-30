using UrlShortener.Models;
using UrlShortener.Repository;
using UrlShortener.Services;

namespace UrlShortener.ModelBuilder
{
    public class ShortenedUrlBuilder : IShortenedUrlBuilder
    {
        private readonly IShortenUrlRepository _shortenedUrlRepository;
        private readonly IRandomUrlGenerator _randomUrlGenerator;

        public ShortenedUrlBuilder(IShortenUrlRepository shortenedUrlRepository, IRandomUrlGenerator randomUrlGenerator)
        {
            _shortenedUrlRepository = shortenedUrlRepository;
            _randomUrlGenerator = randomUrlGenerator;
        }

        public ShortenedUrl Build(UrlToShorten urlToShorten)
        {
            var result = _shortenedUrlRepository.GetByLongUrl(urlToShorten.Url);

            if (result is null)
            {
                result = new ShortenedUrl
                {
                    Url = urlToShorten.Url,
                    ShortUrl = _randomUrlGenerator.Generate()
                };

                _shortenedUrlRepository.Add(result);
            }

            return result;
        }
    }
}
