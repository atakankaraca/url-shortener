namespace UrlShortener.Api.Models;

public class CreateShortUrlResponse
{
    public CreateShortUrlResponse(string applicationUrl, ShortUrl shortUrl)
    {
        ShortUrl = $"{applicationUrl}/{shortUrl.ShortCode}";
        DestinationUrl = shortUrl.DestinationUrl;
    }

    public string ShortUrl { get; set; }
    public string DestinationUrl { get; set; }
}