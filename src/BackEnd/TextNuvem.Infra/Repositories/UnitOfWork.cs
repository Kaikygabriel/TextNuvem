using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Data.Context;

namespace TextNuvem.Infra.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}