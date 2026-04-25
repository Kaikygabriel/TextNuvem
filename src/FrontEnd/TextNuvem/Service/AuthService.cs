using RestSharp;
using TextNuvem.Commum;
using TextNuvem.Configuration;
using TextNuvem.Dtos.Customers;

namespace TextNuvem.Service;

internal class AuthService: DelegatingHandler
{
    private readonly LocalStorageService _storageService;

    public AuthService(LocalStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<bool> CustomerIsAuthentication()
    {
        return await _storageService.GetToken() is not null;
    }
    
    public async Task<Result> Login(CustomerLoginDto model)
    {
        var request = new RestRequest($"{ApiConfiguration.BaseUrl}/Customer/Login",Method.Post)
            .AddJsonBody(model);
        
        var client = new RestClient();
        var response = await client.ExecuteAsync<AuthCustomerResponse>(request);
        if (!response.IsSuccessStatusCode)
            return Result.Failure(response.ErrorMessage!);
    
        await _storageService.AddAuthCustomerInStorage(response.Data!);
        
        return Result.Success();
    }
    
    public async Task<Result> Register(CustomerRegisterDto model)
    {
        var request = new RestRequest($"{ApiConfiguration.BaseUrl}/Customer/Register",Method.Post)
            .AddJsonBody(model);
        
        var client = new RestClient();
        var response = await client.ExecuteAsync<AuthCustomerResponse>(request);
        if (!response.IsSuccessStatusCode)
            return Result.Failure(response.ErrorMessage!);
    
        await _storageService.AddAuthCustomerInStorage(response.Data!);
        
        return Result.Success();
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _storageService.GetToken();
          
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
          
        return await base.SendAsync(request, cancellationToken);
    } 
 
}