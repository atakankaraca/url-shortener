namespace UrlShortener.Api.Extensions;

public static class ShortCodeExtensions 
{
    public static readonly char[] AlphanumericCharacters = { '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };
    public static readonly char[] SafeSpecialCharacters = { '-', '_' };
    public static readonly char[] ValidCharacterSet = AlphanumericCharacters.Concat(SafeSpecialCharacters).ToArray();
    public static readonly long Base = AlphanumericCharacters.Length;

    public static string ToShortCode(this long value)
    {
        if (value <= 0) 
            throw new OperationException("Value cannot be less than 0.");

        var stringBuilder = new StringBuilder();

        while (value > 0)
        {
            stringBuilder.Insert(0, AlphanumericCharacters[Convert.ToInt32(value % Base)]);
            value = value / Base;
        }

        return stringBuilder.ToString();
    }
}