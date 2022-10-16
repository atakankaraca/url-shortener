namespace UrlShortener.Api.UnitTests.Extensions;

public class ShortCodeExtensionsTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ToShortCode_Should_Throw_Exception_When_Value_Less_Than_Or_Equal_To_Zero(long value)
    {
        Assert.Throws<OperationException>(() => value.ToShortCode());
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(256, "48")]
    [InlineData(9997447875, "aUAgTh")]
    public void ToShortCode_Should_Success(long value, string expectedShortCode)
    {
        var actualShortCode = value.ToShortCode();
        Assert.Equal(expectedShortCode, actualShortCode);
    }
}