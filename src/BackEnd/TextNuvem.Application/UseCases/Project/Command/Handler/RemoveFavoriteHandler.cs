using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Project.Command.Handler;

internal sealed class RemoveFavoriteHandler : IRequestHandler<RemoveFavoriteRequest,Result>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFavoriteHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveFavoriteRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.ProjectId);
        if (project is null)
            return new Error("Project not found !");
        project.RemoveFavorite();
        _projectRepository.Update(project);
        await _unitOfWork.CommitAsync();
        
        return Result.Success();
    }
}