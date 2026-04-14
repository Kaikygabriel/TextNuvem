using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Domain.BackOffice.Repositories;

public interface ICustomerRepository
{
    Task<bool> Exists(string email);
    Task<Customer?> GetById(Guid id);
    Task<Customer?> GetByEmail(string email);

    void Create(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}