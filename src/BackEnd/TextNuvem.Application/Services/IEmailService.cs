using TextNuvem.Application.Dtos;
using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Application.Services;

public interface IEmailService
{
    Task<Result> Send(EmailRequest request);
}