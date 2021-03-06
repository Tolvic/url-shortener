using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UrlShortener.Models
{
    public class UrlToShorten
    {
        private const string urlRegex = @"^http(s?):\/\/(www\.)?[a-zA-Z0-9@:%._\\+~#=]{2,256}.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)$";

        private readonly string _url;

        [Required(ErrorMessage = "Please enter a URL")]
        [RegularExpression(urlRegex, ErrorMessage = "Please enter a valid URL. A Url must start with either https:// or  http:// and must end with a TLD such as .com or .co.uk")]
        public string Url {
            get => _url;
            init => _url = HttpUtility.HtmlEncode(value);
        }
    }
}
