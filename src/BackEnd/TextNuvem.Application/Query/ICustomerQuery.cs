using TextNuvem.Application.Dtos.Customers;

namespace TextNuvem.Application.Query;

public interface ICustomerQuery
{
    Task<CustomerDashBoard> GetDashBoardById(Guid id);
}//Vai Salvar os Folders do project como TEXT > COMPRIMIR -> JSON -> FOlder object 