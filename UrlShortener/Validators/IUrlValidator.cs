namespace UrlShortener.Validators
{
    public interface IUrlValidator
    {
        public bool IsUrl(string urlToValidate);
    }
}