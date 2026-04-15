namespace TextNuvem.Application.UseCases.Customers.Command.Response;

public record AuthCustomerResponse(string Token,string RefreshToken,Guid CustomerId);