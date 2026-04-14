using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Application.UseCases.Customer.Command.Request;


public record RegisterCustomerRequest(string Password,string Email,string Name)
{
    public Result<Domain.BackOffice.Entities.Customer> ToEntity()
    {
        var resultCreatePassword = Domain.BackOffice.ValueObject.Password.Factory.Create(Password);
        if (!resultCreatePassword.IsSuccess)
            return resultCreatePassword.Error;
        
        var resultCreateEmail = Domain.BackOffice.ValueObject.Email.Factory.Create(Email);
        if (!resultCreateEmail.IsSuccess)
            return resultCreateEmail.Error;

        return new Domain.BackOffice.Entities.Customer(resultCreateEmail.Value, resultCreatePassword.Value, Name);
    }
}