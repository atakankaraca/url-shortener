namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("/")]
public class RedirectionController : ControllerBase
{
    private readonly IUrlShortenerService _urlShortenerService;
    
    public RedirectionController(IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpGet("{shortCode}")]
    public IActionResult RedirectToDestinationUrl(string shortCode)
    {
        var shortUrl = _urlShortenerService.GetByShortCode(shortCode);
        if(shortUrl == null)
            throw new NotFoundException("Short url not found.");

        return RedirectPermanent(shortUrl.DestinationUrl);
    }
}
