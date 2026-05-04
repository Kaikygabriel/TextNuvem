using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customers.Command.Request;

public record GetLastProjectUpdateRequest(Guid CustomerId) : IRequest<Result<Guid?>>;