namespace UrlShortener.Api.Services.UrlShortenerService;

public class UrlShortenerService : IUrlShortenerService
{   
    private readonly IShortUrlStorage _shortUrlStorage;
    public UrlShortenerService(IShortUrlStorage shortUrlStorage)
    {
        _shortUrlStorage = shortUrlStorage;
    }

    public ShortUrl CreateShortUrl(string destinationUrl)
    {
        var nextShortCode = _shortUrlStorage.GetNextShortCode();
        var shortUrl = new ShortUrl(nextShortCode, destinationUrl);

        _shortUrlStorage.Add(shortUrl);

        return shortUrl;
    }

    public ShortUrl CreateCustomShortUrl(string customShortCode, string destinationUrl) 
    {
        if(_shortUrlStorage.IsExists(customShortCode))
            throw new ConflictException("Custom short url is already taken.");

        var shortUrl = new ShortUrl(customShortCode, destinationUrl, isCustom: true);

        _shortUrlStorage.Add(shortUrl);

        return shortUrl;
    }

    public ShortUrl GetByShortCode(string shortCode)
    {
        if(!_shortUrlStorage.IsExists(shortCode))
            return null;
        else
            return _shortUrlStorage.Get(shortCode);
    }
}