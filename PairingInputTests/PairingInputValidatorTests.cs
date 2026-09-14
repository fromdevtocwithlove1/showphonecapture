using ScrcpyController;
using Xunit;

namespace PairingInputTests;

public class PairingInputValidatorTests
{
    [Fact]
    public void AcceptsValidIpv4EndpointAndPairingCode()
    {
        var result = PairingInputValidator.Validate("192.168.1.100:37123", "123456");

        Assert.True(result.IsValid);
        Assert.Equal("192.168.1.100:37123", result.Endpoint);
        Assert.Equal("123456", result.PairingCode);
    }

    [Theory]
    [InlineData("192.168.1.100", "123456")]
    [InlineData("192.168.1.100:0", "123456")]
    [InlineData("192.168.1.100:65536", "123456")]
    [InlineData("192.168.1.100:37123", "")]
    [InlineData("192.168.1.100:37123", "12ab56")]
    [InlineData("192.168.1.100:37123", "12345")]
    [InlineData("192.168.1.100:37123", "１２３４５６")]
    public void RejectsInvalidPairingInput(string endpoint, string pairingCode)
    {
        var result = PairingInputValidator.Validate(endpoint, pairingCode);

        Assert.False(result.IsValid);
        Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
    }
}
