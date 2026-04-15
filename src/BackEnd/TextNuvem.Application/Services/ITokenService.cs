using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Application.Services;

public interface ITokenService
{
    string GenerateAccessToken(Customer customer);
    string GenerateRefreshToken();
}