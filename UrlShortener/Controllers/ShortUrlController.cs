using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    public class ShortUrlController : Controller
    {
        [HttpPost]
        public IActionResult Shorten()
        {
            return Ok();
        }
    }
}
