namespace UrlShortener.Api.UnitTests.Extensions;

public class UrlExtensionsTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void IsValidShortPath_Should_Return_False_When_Path_Null_Or_Empty(string value)
    {
        var result = UrlExtensions.IsValidShortPath(value);
        Assert.False(result);
    }
        
    [Theory]
    [InlineData("ui12dAc")]
    [InlineData("some-long-value")]
    public void IsValidShortPath_Should_Return_False_When_Path_Lenght_Greater_Than_Max_Short_Path_Lenght(string value)
    {
        var result = UrlExtensions.IsValidShortPath(value);
        Assert.False(result);
    }

    [Theory]
    [InlineData("@")]
    [InlineData("/")]
    [InlineData("'")]
    [InlineData("$")]
    public void IsValidShortPath_Should_Return_False_When_Path_Contains_Unsafe_Characters(string value)
    {
        var result = UrlExtensions.IsValidShortPath(value);
        Assert.False(result);
    }

    [Theory]
    [InlineData("assign")]
    [InlineData("jobs")]
    [InlineData("as49DJ")]
    [InlineData("1")]
    public void IsValidShortPath_Should_Return_True(string value)
    {
        var result = UrlExtensions.IsValidShortPath(value);
        Assert.True(result);
    }
}