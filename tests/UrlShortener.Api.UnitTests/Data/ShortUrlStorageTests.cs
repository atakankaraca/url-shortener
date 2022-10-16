namespace UrlShortener.Api.UnitTests.Data;

public class ShortUrlStorageTests : IClassFixture<StorageFixture>
{
    StorageFixture _storageFixture;
    ShortUrlStorage _shortUrlStorage;

    public ShortUrlStorageTests(StorageFixture storageFixture)
    {
        _storageFixture = storageFixture;
        _shortUrlStorage = storageFixture.ShortUrlStorage;
    }


    [Fact]
    public void Get_Should_Return_ShortUrl()
    {
        var shortCode = "asdf";
        _shortUrlStorage.Add(new ShortUrl(shortCode, "https://www.google.com"));

        Assert.NotNull(_shortUrlStorage.Get(shortCode));
    }

    [Fact]
    public void Get_Should_Throws_Exception_When_ShortUrl_Not_Found()
    {
        var shortCode = "adsd1f";

        Assert.Throws<KeyNotFoundException>(() => _shortUrlStorage.Get(shortCode));
    }

    [Fact]
    public void IsExists_Should_Return_False() 
    {
        var shortCode = "as1dfa";

        var result = _shortUrlStorage.IsExists(shortCode);
        Assert.False(result);
    }

    [Fact]
    public void IsExists_Should_Return_True() 
    {
        var shortCode = "a14daf";
        _shortUrlStorage.Add(new ShortUrl(shortCode, "https://www.google.com"));

        var result = _shortUrlStorage.IsExists(shortCode);
        Assert.True(result);
    }

    [Fact]
    public void Add_Should_Success() 
    {
        var shortCode = "1a4daf";
        _shortUrlStorage.Add(new ShortUrl(shortCode, "https://www.google.com"));

        Assert.True(_shortUrlStorage.IsExists(shortCode));
    }

    [Fact]
    public void GetNextShortCode_Should_Return_New_Short_Code_Next_Short_Code_Used_By_Custom_Short_Code_Selection() 
    {
        var customShortCode = "1";
        _shortUrlStorage.Add(new ShortUrl(customShortCode, "https://www.google.com", isCustom: true));

        var expectedShortCode = "2";
        var actualShortCode = _shortUrlStorage.GetNextShortCode();

        Assert.Equal(expectedShortCode, actualShortCode);
    }

    [Fact]
    public void GetNextShortCode_Should_Return_New_Short_Code_When_Short_Code_Not_Exists() 
    {
        var expectedShortCode = "1";
        var actualShortCode = _shortUrlStorage.GetNextShortCode();

        Assert.Equal(expectedShortCode, actualShortCode);
    }
}