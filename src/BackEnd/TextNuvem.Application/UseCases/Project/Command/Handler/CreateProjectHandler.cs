using MediatR;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Project.Command.Handler;

internal sealed class CreateProjectHandler : IRequestHandler<CreateProjectRequest,Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectHandler(IProjectRepository projectRepository, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer is null)
            return new Error("Customer not found!");

        var newProject = new Domain.BackOffice.Entities.Project(request.Name, customer);

        var resultAddInCustomer = customer.AddProject(newProject);
        if (!resultAddInCustomer.IsSuccess)
            return resultAddInCustomer.Error;
        
        _projectRepository.Create(newProject);
        _customerRepository.Update(customer);

        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }
}