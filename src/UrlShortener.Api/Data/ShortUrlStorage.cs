namespace UrlShortener.Api.Data;

public class ShortUrlStorage : IShortUrlStorage
{
    private readonly Dictionary<string, ShortUrl> _shortUrlStorage = new();
    private long _counter = 1;

    public ShortUrl Get(string shortUrl) => _shortUrlStorage[shortUrl];

    public bool IsExists(string shortCode) => _shortUrlStorage.ContainsKey(shortCode);
    
    public void Add(ShortUrl shortUrl)
    {
        _shortUrlStorage.Add(shortUrl.ShortCode, shortUrl);

        if(shortUrl.IsCustom)
            _counter++;
    } 

    public string GetNextShortCode()
    {
        while(true)
        {
            var shortCode = _counter.ToShortCode();
            var isExists = IsExists(shortCode);
            
            if(isExists)
            {
                _counter++;
                continue;
            }
            else
            {
                return shortCode;
            }
        }
    }    
}