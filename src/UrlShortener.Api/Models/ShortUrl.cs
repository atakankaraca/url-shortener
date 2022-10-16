public class ShortUrl 
{
    public ShortUrl(string shortCode, string destinationUrl, bool isCustom = false)
    {
        if(shortCode.Length > ShortCodeConst.MaxShortCodeLenght)
            throw new OperationException("Max short code lenght exceeded.");

        ShortCode = shortCode;
        DestinationUrl = destinationUrl.Trim();
        IsCustom = isCustom;
    }

    public string ShortCode { get; private set; }
    public string DestinationUrl { get; private set; }
    public bool IsCustom { get; private set; }
}