using MediatR;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Application.UseCases.Customers.Command.Request;


public record RegisterCustomerRequest(string Password,string Email,string Name) 
    : IRequest<Result<AuthCustomerResponse>>
{
    public Result<Customer> ToEntity()
    {
        var resultCreatePassword = Domain.BackOffice.ValueObject.Password.Factory.Create(Password);
        if (!resultCreatePassword.IsSuccess)
            return resultCreatePassword.Error;
        
        var resultCreateEmail = Domain.BackOffice.ValueObject.Email.Factory.Create(Email);
        if (!resultCreateEmail.IsSuccess)
            return resultCreateEmail.Error;

        return new Customer(resultCreateEmail.Value, resultCreatePassword.Value, Name);
    }
}