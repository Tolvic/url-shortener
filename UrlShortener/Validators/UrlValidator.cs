using System;

namespace UrlShortener.Validators
{
    public class UrlValidator : IUrlValidator
    {
        public bool IsUrl(string urlToValidate)
        {
            Uri uriBeingValidated;

            var isValid =  !string.IsNullOrWhiteSpace(urlToValidate) &&
                    IsEscaped(urlToValidate) &&
                    CanCreateUri(urlToValidate, out uriBeingValidated) &&
                    hasScheme(uriBeingValidated);

            return isValid;
        }

        private bool CanCreateUri(string urlToValidate, out Uri urlBeingValidated)
        {
            return Uri.TryCreate(urlToValidate, UriKind.Absolute, out urlBeingValidated);
        }

        private bool hasScheme(Uri url)
        {
            return url.Scheme == "https" || url.Scheme == "http";
        }

        private bool IsEscaped(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
