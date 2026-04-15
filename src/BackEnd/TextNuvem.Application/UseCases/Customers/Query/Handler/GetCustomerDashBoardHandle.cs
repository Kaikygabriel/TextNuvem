using TextNuvem.Application.Dtos.Customers;
using TextNuvem.Application.Query;
using TextNuvem.Application.UseCases.Customers.Query.Request;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customers.Query.Handler;

internal sealed class GetCustomerDashBoardHandle :IRequestHandler<GetCustomerDashBoardRequest,Result<CustomerDashBoard>>
{
    private readonly ICustomerQuery _customerQuery;

    public GetCustomerDashBoardHandle(ICustomerQuery customerQuery)
    {
        _customerQuery = customerQuery;
    }

    public async Task<Result<CustomerDashBoard>> Handle(GetCustomerDashBoardRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerQuery.GetDashBoardById(request.CustomerId);
        if (customer is null)
            return new Error("Customer not found !");
        
        return customer;
    }
}