using MediatR;
using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.UseCases.Project.Command.Request;

public record RemoveProjectRequest(Guid CustomerId,Guid ProjectId) : IRequest<Result>;