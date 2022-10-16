namespace UrlShortener.Api.UnitTests.Data;

public class StorageFixture : IDisposable
{
    public StorageFixture()
    {
        ShortUrlStorage = new ShortUrlStorage();
    }

    public void Dispose()
    {
        ShortUrlStorage = new ShortUrlStorage();        
    }

    public ShortUrlStorage ShortUrlStorage { get; private set; }
}