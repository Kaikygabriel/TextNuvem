using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Domain.BackOffice.ValueObject;

public record RefreshToken(string Token)
{
    public string Token { get; private init; } = Token;
    public DateTime Expired { get; private init; } = DateTime.UtcNow.AddHours(16);

    public Result VerifyRefreshToken(string token)
        => Token.Equals(token) ? Result.Success() : new Error("Token is invalid !");
}