namespace UrlShortener.Models
{
    public class ShortenedUrl : UrlToShorten
    {
        public int Id { get; set; }
        public string ShortUrl { get; set; }
    }
}
