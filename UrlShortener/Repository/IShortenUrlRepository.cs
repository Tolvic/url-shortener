using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public interface IShortenUrlRepository
    {
        public ShortenedUrl GetByShortUrl(string shortUrl);

        public ShortenedUrl GetByLongUrl(string longUrl);

        public void Add(ShortenedUrl shortenUrl);
    }
}