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
        => await _appDbContext.Customers.Select(x => new
            CustomerDashBoard(
                x.Id
                ,ProjectDto.ToProjectDtos(x.Projects)
                ,x.Name
                ,x.Email.Address))
            .FirstOrDefaultAsync(x=>x.Id == id);
    
}