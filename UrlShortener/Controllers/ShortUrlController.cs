using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class ShortUrlController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shorten(UrlToShorten urlToShorten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
