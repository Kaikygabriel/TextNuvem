using TextNuvem.Application.Dtos.Customers;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customers.Query.Request;

public record GetCustomerDashBoardRequest(Guid CustomerId) : IRequest<Result<CustomerDashBoard>>;