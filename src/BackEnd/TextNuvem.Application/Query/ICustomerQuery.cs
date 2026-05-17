using TextNuvem.Application.Dtos.Customers;

namespace TextNuvem.Application.Query;

public interface ICustomerQuery
{
    Task<CustomerDashBoard?> GetDashBoardById(Guid id,CancellationToken cancellationToken = default);
} 