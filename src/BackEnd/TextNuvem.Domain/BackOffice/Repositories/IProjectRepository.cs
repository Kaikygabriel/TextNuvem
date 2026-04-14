using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Domain.BackOffice.Repositories;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllByCustomer(Guid customerId);
    Task<Project?> GetById(Guid id);
    Task<bool> Exists(string name);
    
    void Create(Project project);
    void Update(Project project);
    void Delete(Project project);
}