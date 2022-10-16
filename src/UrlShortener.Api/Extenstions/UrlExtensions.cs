using UrlShortener.Api.Constants;

namespace UrlShortener.Api.Extensions;

public static class UrlExtensions 
{
    public static bool IsValidShortPath(this string path)
    {
        if(string.IsNullOrEmpty(path))
            return false;

        if(path.Length > ShortCodeConst.MaxShortCodeLenght)
            return false;

        foreach(var character in path)
        {
            if(!ShortCodeExtensions.ValidCharacterSet.Contains(character))
                return false;
        }

        return true;
    }
}