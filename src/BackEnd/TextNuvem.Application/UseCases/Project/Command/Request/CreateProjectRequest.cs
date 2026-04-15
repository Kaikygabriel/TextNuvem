using MediatR;
using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.UseCases.Project.Command.Request;

public record CreateProjectRequest(string Name,Guid CustomerId): IRequest<Result>;