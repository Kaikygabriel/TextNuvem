using TextNuvem.Application.UseCases.Customers.Command.Request;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Customers.Command.Handler;

internal sealed class GetLastProjectUpdateHandler : IRequestHandler<GetLastProjectUpdateRequest,Result<Guid?>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetLastProjectUpdateHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<Guid?>> Handle(GetLastProjectUpdateRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer is null)
            return new Error("Customer.NotFound");
        var lastProject = customer.LastProjectIdUpdate;
        return lastProject;
    }
}