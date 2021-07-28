using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class Url
    {
        private const string urlRegex = @"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";

        [Required(ErrorMessage = "Please enter a URL")]
        [RegularExpression(urlRegex, ErrorMessage = "Please enter a valid URL.")]
        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }
    }
}
