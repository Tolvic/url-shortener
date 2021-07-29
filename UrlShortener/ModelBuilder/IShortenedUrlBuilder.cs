using UrlShortener.Models;

namespace UrlShortener.ModelBuilder
{
    public interface IShortenedUrlBuilder
    {
        public ShortenedUrl Build(UrlToShorten urlToShorten);
    }
}