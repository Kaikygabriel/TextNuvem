namespace TextNuvem.Application.UseCases.Customer.Command.Response;

public record AuthCustomerResponse(string Token,string RefreshToken,Guid CustomerId);