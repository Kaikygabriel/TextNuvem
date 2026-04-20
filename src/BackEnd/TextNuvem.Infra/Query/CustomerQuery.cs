using Microsoft.EntityFrameworkCore;
using TextNuvem.Application.Dtos.Customers;
using TextNuvem.Application.Dtos.Projects;
using TextNuvem.Application.Query;
using TextNuvem.Infra.Data.Context;

namespace TextNuvem.Infra.Query;

internal sealed class CustomerQuery : ICustomerQuery
{
    private readonly AppDbContext _appDbContext;

    public CustomerQuery(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CustomerDashBoard?> GetDashBoardById(Guid id)
    {
        var customer = await _appDbContext.Customers
                .Include(x=>x.Projects)
                .FirstOrDefaultAsync(x=>x.Id == id);
        
        if (customer == null)
            return null;

        return new CustomerDashBoard(
            customer.Id,
            ProjectDto.ToProjectDtos(customer.Projects),
            customer.Name,
            customer.Email.Address
        );
    }
    
}