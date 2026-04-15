using TextNuvem.Application.Dtos.Projects;
using TextNuvem.Application.Query;
using TextNuvem.Application.UseCases.Project.Query.Request;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Project.Query.Handler;

internal sealed class GetProjectHandler: IRequestHandler<GetProjectRequest,Result<GetProjectDto>>
{
    private readonly IProjectQuery _projectQuery;

    public GetProjectHandler(IProjectQuery projectQuery)
    {
        _projectQuery = projectQuery;
    }

    public async Task<Result<GetProjectDto>> Handle(GetProjectRequest request, CancellationToken cancellationToken)
    {
        var projectDto =  await _projectQuery.GetById(request.ProjectId);
        if (projectDto is null)
            return new Error("Project not found!");
        return projectDto;
    }
}