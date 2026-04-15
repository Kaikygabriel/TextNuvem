using TextNuvem.Application.Dtos.Projects;
using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Project.Query.Request;

public record GetProjectRequest(Guid ProjectId) : IRequest<Result<GetProjectDto>>;