using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.UseCases.Project.Command.Request;

public record UpdateFilesInProjectRequest(string CompressFolders,Guid ProjectId) : IRequest<Result>;