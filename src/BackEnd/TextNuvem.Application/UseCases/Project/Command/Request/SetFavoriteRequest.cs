using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.UseCases.Project.Command.Request;

public record SetFavoriteRequest(Guid ProjectId) : IRequest<Result>;