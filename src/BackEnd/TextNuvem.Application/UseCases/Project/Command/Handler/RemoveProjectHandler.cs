using MediatR;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Project.Command.Handler;

internal sealed class RemoveProjectHandler : IRequestHandler<RemoveProjectRequest,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public RemoveProjectHandler( IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Result> Handle(RemoveProjectRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer is null)
            return new Error("Project or Customer, not found");

        var resultRemoveProject = customer.RemoveProject(request.ProjectId);
        if (!resultRemoveProject.IsSuccess)
            return resultRemoveProject.Error;

        _customerRepository.Update(customer);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}