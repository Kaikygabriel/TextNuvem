using TextNuvem.Application.Dtos.Projects;

namespace TextNuvem.Application.Query;

public interface IProjectQuery
{
    Task<GetProjectDto?> GetById(Guid id); 
}