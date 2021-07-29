using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Models
{
    public class ShortenedUrl : UrlToShorten
    {
        public string ShortUrl { get; set; }
    }
}
