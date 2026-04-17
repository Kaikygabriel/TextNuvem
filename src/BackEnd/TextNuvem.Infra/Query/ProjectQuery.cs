using Microsoft.EntityFrameworkCore;
using TextNuvem.Application.Dtos.Projects;
using TextNuvem.Application.Query;
using TextNuvem.Application.Services;
using TextNuvem.Infra.Data.Context;

namespace TextNuvem.Infra.Query;

internal sealed class ProjectQuery : IProjectQuery
{
    private readonly AppDbContext _appDbContext;
    private readonly ICompactorService _compactorService;
    
    public ProjectQuery(AppDbContext appDbContext, ICompactorService compactorService)
    {
        _appDbContext = appDbContext;
        _compactorService = compactorService;
    }

    public async Task<GetProjectDto?> GetById(Guid id)
    {
        var project = await _appDbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (project is null)
            return null;
        Console.WriteLine(string.Join(',',project.Folders.Select(x=>x.Path)));
        return  new GetProjectDto(
            project.Id,
            project.Name,
            project.LastUpdate,
            _compactorService.CompressObject(project.Folders));
    }
}