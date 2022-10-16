namespace UrlShortener.Api.Services.UrlShortenerService;

public interface IUrlShortenerService
{
    public ShortUrl CreateShortUrl(string destinationUrl);
    public ShortUrl CreateCustomShortUrl(string customShortCode, string destinationUrl);
    public ShortUrl GetByShortCode(string shortCode);
}