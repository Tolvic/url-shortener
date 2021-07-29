using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrl { get; set; }
    }
}
