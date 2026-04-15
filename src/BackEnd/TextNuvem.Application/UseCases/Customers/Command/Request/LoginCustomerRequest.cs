using MediatR;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customers.Command.Request;

public record LoginCustomerRequest(string Email,string Password) : IRequest<Result<AuthCustomerResponse>>;