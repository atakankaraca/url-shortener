namespace UrlShortener.Api.Controllers;

public class ShortUrlsController : BaseController
{
    private readonly IUrlShortenerService _urlShortenerService;
    private readonly ApplicationSettings _applicationSettings;

    public ShortUrlsController(
        IUrlShortenerService urlShortenerService,
        ApplicationSettings applicationSettings)
    {
        _urlShortenerService = urlShortenerService;
        _applicationSettings = applicationSettings;
    }

    [HttpPost]
    public IActionResult CreateShortUrl(CreateShortUrlRequest request)
    {
        var validator = new CreateShortUrlRequestValidator();
        validator.ValidateAndThrow(request);

        var shortUrl = default(ShortUrl);

        if(request.HasCustomShortPath)
            shortUrl = _urlShortenerService.CreateCustomShortUrl(request.CustomShortPath, request.DestinationUrl);
        else
            shortUrl = _urlShortenerService.CreateShortUrl(request.DestinationUrl);

        if(shortUrl == null)
            throw new OperationException("Short url creation failed.");

        return Ok(new CreateShortUrlResponse(_applicationSettings.ApplicationUrl, shortUrl));
    }
}
