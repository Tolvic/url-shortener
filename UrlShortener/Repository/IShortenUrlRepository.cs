using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public interface IShortenUrlRepository
    {
        public string GetLongUrl(string shortUrl);

        public string GetShortUrl(string shortUrl);

        public void Add(ShortenedUrl shortenUrl);
    }
}