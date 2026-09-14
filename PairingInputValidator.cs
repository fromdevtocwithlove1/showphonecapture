using System.Net;

namespace ScrcpyController;

public sealed class PairingInputResult
{
    public bool IsValid { get; init; }
    public string Endpoint { get; init; } = string.Empty;
    public string PairingCode { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;
}

public static class PairingInputValidator
{
    public static PairingInputResult Validate(string? endpointInput, string? pairingCodeInput)
    {
        string endpoint = (endpointInput ?? string.Empty).Trim();
        string pairingCode = (pairingCodeInput ?? string.Empty).Trim();
        int separator = endpoint.LastIndexOf(':');

        if (separator <= 0 || separator == endpoint.Length - 1
            || !IPAddress.TryParse(endpoint[..separator], out var address)
            || address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork
            || !int.TryParse(endpoint[(separator + 1)..], out int port)
            || port is < 1 or > 65535)
        {
            return Invalid("Endpoint phải có dạng IP:cổng, ví dụ 192.168.1.100:37123.");
        }

        if (pairingCode.Length != 6 || pairingCode.Any(c => c < '0' || c > '9'))
        {
            return Invalid("Mã ghép đôi chỉ được chứa chữ số.");
        }

        return new PairingInputResult
        {
            IsValid = true,
            Endpoint = endpoint,
            PairingCode = pairingCode
        };
    }

    private static PairingInputResult Invalid(string message) => new() { ErrorMessage = message };
}
