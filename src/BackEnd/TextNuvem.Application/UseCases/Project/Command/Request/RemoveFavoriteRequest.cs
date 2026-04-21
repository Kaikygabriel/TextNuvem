using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.UseCases.Project.Command.Request;

public record RemoveFavoriteRequest(Guid ProjectId) : IRequest<Result>;