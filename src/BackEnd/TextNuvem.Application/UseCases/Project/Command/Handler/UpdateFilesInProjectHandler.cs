using MediatR;
using TextNuvem.Application.Dtos;
using TextNuvem.Application.Ioc.Folders;
using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.Repositories;

namespace TextNuvem.Application.UseCases.Project.Command.Handler;

internal sealed class UpdateFilesInProjectHandler : IRequestHandler<UpdateFilesInProjectRequest,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly ICompactorService _compactorService;
    
    public UpdateFilesInProjectHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, ICompactorService compactorService)
    {
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
        _compactorService = compactorService;
    }

    public async Task<Result> Handle(UpdateFilesInProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.ProjectId);
        if (project is null)
            return new Error("Project not found!");

        var folders = _compactorService.DecompressObject<List<FolderDatabaseDto>>(request.CompressFolders)
            .Select(x => (Folder)x).ToList();
     
        project.UpdateFolders(folders);
        
        _projectRepository.Update(project);
        await _unitOfWork.CommitAsync();

        return Result.Success();
    }
}