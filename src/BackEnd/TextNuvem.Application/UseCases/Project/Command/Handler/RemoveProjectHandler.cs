using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Project.Command.Handler;

internal sealed class RemoveProjectHandler : IRequestHandler<RemoveProjectRequest,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProjectRepository _projectRepository;
    
    public RemoveProjectHandler( IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IProjectRepository projectRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result> Handle(RemoveProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.ProjectId);
        if(project is null)
            return new Error("Project or Customer, not found");
        
        if(project.CustomerId != request.CustomerId)
            return new Error("Project or Customer, not found");
        
        _projectRepository.Delete(project);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}