using Microsoft.AspNetCore.Mvc;
using UrlShortener.ModelBuilder;
using UrlShortener.Models;
using UrlShortener.Validators;

namespace UrlShortener.Controllers
{
    public class ShortUrlController : Controller
    {
        private readonly IUrlValidator _urlValidator;
        private readonly IShortenedUrlBuilder _shortenedUrlBuilder;

        public ShortUrlController(IUrlValidator urlValidator, IShortenedUrlBuilder shortenedUrlBuilder)
        {
            _urlValidator = urlValidator;
            _shortenedUrlBuilder = shortenedUrlBuilder;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shorten(UrlToShorten urlToShorten)
        {
            if (!ModelState.IsValid || !_urlValidator.IsUrl(urlToShorten.Url))
            {
                return BadRequest();
            }

            var shortenedUrl = _shortenedUrlBuilder.Build(urlToShorten);

            return Ok(shortenedUrl.ShortUrl);
        }
    }
}
