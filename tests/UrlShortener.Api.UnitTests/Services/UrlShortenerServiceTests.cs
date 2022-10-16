namespace UrlShortener.Api.UnitTests.Services;

public class UrlShortenerServiceTests : IClassFixture<StorageFixture>
{
    private readonly Mock<IShortUrlStorage> _mockShortUrlStorage;
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlShortenerServiceTests()
    {
        _mockShortUrlStorage = new Mock<IShortUrlStorage>();
        _urlShortenerService = new UrlShortenerService(_mockShortUrlStorage.Object);
    }

    [Fact]
    public void CreateShortUrl_Should_Throw_Exception_When_Next_ShortCode_Is_GreaterThan_MaxShortCodeLenght()
    {
        var nextShortCode = "someLongShortCode";
        _mockShortUrlStorage.Setup(x => x.GetNextShortCode()).Returns(nextShortCode);
        
        Assert.Throws<OperationException>(() => _urlShortenerService.CreateShortUrl("http://www.google.com"));
    }

    [Fact]
    public void CreateShortUrl_Should_Success()
    {
        var nextShortCode = "a85qa9";
        _mockShortUrlStorage.Setup(x => x.GetNextShortCode()).Returns(nextShortCode);
        
        var result = _urlShortenerService.CreateShortUrl("http://www.google.com");
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateCustomShortUrl_Should_Throw_Exception_When_Next_ShortCode_Is_GreaterThan_MaxShortCodeLenght()
    {
        var customShortCode = "some-long-custom-short-code";        
        Assert.Throws<OperationException>(() => _urlShortenerService.CreateCustomShortUrl(customShortCode, "http://www.google.com"));
    }

    [Fact]
    public void CreateCustomShortUrl_Should_Throw_Exception_When_CustomShortCode_Is_Already_In_Use()
    {
        var customShortCode = "jobs";
        
        _mockShortUrlStorage.Setup(x => x.IsExists(customShortCode)).Returns(true);
        Assert.Throws<ConflictException>(() => _urlShortenerService.CreateCustomShortUrl(customShortCode, "http://www.google.com"));
    }

    [Fact]
    public void CreateCustomShortUrl_Should_Success()
    {
        var customShortCode = "jobs";
        
        _mockShortUrlStorage.Setup(x => x.IsExists(customShortCode)).Returns(false);
        
        var result = _urlShortenerService.CreateCustomShortUrl(customShortCode, "http://www.google.com");
        Assert.NotNull(result);
    }

    [Fact]
    public void GetByShortCode_Should_Return_Null()
    {
        var shortCode = "jobs";
        
        _mockShortUrlStorage.Setup(x => x.IsExists(shortCode)).Returns(false);
        
        var result = _urlShortenerService.GetByShortCode(shortCode);
        Assert.Null(result);
    }

    [Fact]
    public void GetByShortCode_Should_Return_ShortUrl()
    {
        var shortCode = "jobs";
        
        _mockShortUrlStorage.Setup(x => x.IsExists(shortCode)).Returns(true);
        _mockShortUrlStorage.Setup(x => x.Get(shortCode)).Returns(new ShortUrl(shortCode, "http://www.google.com"));
        
        var result = _urlShortenerService.GetByShortCode(shortCode);
        Assert.NotNull(result);
    }
}