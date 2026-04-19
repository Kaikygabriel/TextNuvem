using System.Text.Json.Serialization;

namespace TextNuvem.Dtos.Customers;

public class AuthCustomerResponse(string Token,string RefreshToken,Guid CustomerId)
{
    [JsonPropertyName("token")]
    public string Token { get; init; } = Token;
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; init; } = RefreshToken;
    [JsonPropertyName("customerId")]
    public Guid CustomerId { get; init; } = CustomerId;

    public void Deconstruct(out string Token, out string RefreshToken, out Guid CustomerId)
    {
        Token = this.Token;
        RefreshToken = this.RefreshToken;
        CustomerId = this.CustomerId;
    }
}