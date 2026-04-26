using Microsoft.EntityFrameworkCore;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Data.Context;

namespace TextNuvem.Infra.Repositories;

internal sealed class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _appDbContext;

    public ProjectRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Project>> GetAllByCustomer(Guid customerId)
    {
        return await _appDbContext.Projects
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Customer)
            .ToListAsync();
    }

    public async Task<Project?> GetById(Guid id)
    {
        return await _appDbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Exists(string name)
    {
        return await _appDbContext.Projects.AnyAsync(x => x.Name == name);
    }

    public void Create(Project project)
    {
        _appDbContext.Projects.Add(project);
    }

    public void Update(Project project)
    {
       _appDbContext.Entry(project).Property(x => x.Folders).IsModified = true;
        _appDbContext.Projects.Update(project);
    }

    public void Delete(Project project)
    {
        _appDbContext.Projects.Remove(project);
    }
}