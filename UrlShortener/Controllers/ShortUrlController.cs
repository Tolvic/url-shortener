using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShortUrlController(IUrlValidator urlValidator, IShortenedUrlBuilder shortenedUrlBuilder, IHttpContextAccessor httpContextAccessor)
        {
            _urlValidator = urlValidator;
            _shortenedUrlBuilder = shortenedUrlBuilder;
            _httpContextAccessor = httpContextAccessor;
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

            var result = BuildFormattedUrl(shortenedUrl);

            return Ok(result);
        }

        private string BuildFormattedUrl(ShortenedUrl shortenedUrl)
        {
            return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{shortenedUrl.ShortUrl}";
        }
    }
}
