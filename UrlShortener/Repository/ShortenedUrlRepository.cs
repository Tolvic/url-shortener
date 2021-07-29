using System;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public class ShortenedUrlRepository : IShortenUrlRepository
    {
        public string GetLongUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public string GetShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public void Add(ShortenedUrl shortenUrl)
        {

        }
    }
}
