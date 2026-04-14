namespace TextNuvem.Domain.BackOffice.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}