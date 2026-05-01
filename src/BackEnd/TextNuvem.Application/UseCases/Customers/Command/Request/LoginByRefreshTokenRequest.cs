using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customers.Command.Request;

public record LoginByRefreshTokenRequest(string Token, Guid CustomerId) : IRequest<Result<AuthCustomerResponse>>;