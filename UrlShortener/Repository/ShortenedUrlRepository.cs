using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public class ShortenedUrlRepository : IShortenUrlRepository
    {
        private UrlShortenerContext _context;

        public ShortenedUrlRepository(UrlShortenerContext context)
        {
            _context = context;
        }

        public ShortenedUrl GetByShortUrl(string shortUrl)
        {
            return _context.ShortenedUrl.SingleOrDefault(x => x.ShortUrl == shortUrl);
        }

        public ShortenedUrl GetByLongUrl(string longUrl)
        {
            return _context.ShortenedUrl.SingleOrDefault(x => x.Url == longUrl);
        }

        public void Add(ShortenedUrl shortenUrl)
        {            
            _context.Add(shortenUrl);

            _context.SaveChanges();
        }
    }
}
