using Microsoft.EntityFrameworkCore;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Data.Context;

namespace TextNuvem.Infra.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerRepository(AppDbContext appDbContext)
        => _appDbContext = appDbContext; 

    public async Task<bool> Exists(string email)
        => await _appDbContext.Customers.AnyAsync(x => x.Email.Address.Equals(email));

    public async Task<Customer?> GetById(Guid id)
        => await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Customer?> GetByEmail(string email)
        => await _appDbContext.Customers.FirstOrDefaultAsync(x => x.Email.Address == email);

    public void Create(Customer customer)
        => _appDbContext.Customers.Add(customer);

    public void Update(Customer customer)
        => _appDbContext.Customers.Update(customer);

    public void Delete(Customer customer)
        => _appDbContext.Customers.Remove(customer);
}