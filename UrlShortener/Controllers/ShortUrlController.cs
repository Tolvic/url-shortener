using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Validators;

namespace UrlShortener.Controllers
{
    public class ShortUrlController : Controller
    {
        private readonly IUrlValidator _urlValidator;

        public ShortUrlController(IUrlValidator urlValidator)
        {
            _urlValidator = urlValidator;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shorten(UrlToShorten urlToShorten)
        {
            if (!ModelState.IsValid || !_urlValidator.IsUrl(urlToShorten.Url))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
