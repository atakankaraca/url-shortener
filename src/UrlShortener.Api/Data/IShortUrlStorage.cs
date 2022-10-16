namespace UrlShortener.Api.Data;

public interface IShortUrlStorage
{
    public ShortUrl Get(string shortUrl);
    public bool IsExists(string shortCode);
    public void Add(ShortUrl shortUrl);
    public string GetNextShortCode();
}