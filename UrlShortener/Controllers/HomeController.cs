using Microsoft.AspNetCore.Mvc;
using UrlShortener.Repository;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private IShortenUrlRepository _shortenedUrlRepositoy;

        public HomeController(IShortenUrlRepository shortenedUrlRepository)
        {
            _shortenedUrlRepositoy = shortenedUrlRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/{shortUrl}")]
        public IActionResult RedirectShortUrlToRealUrl(string shortUrl)
        {
            var shortenedUrl = _shortenedUrlRepositoy.GetByShortUrl(shortUrl);

            if (shortenedUrl == null)
            {
                return RedirectToActionPermanent("Index");
            }

            return RedirectPermanent(shortenedUrl.Url);
        }
    }
}
