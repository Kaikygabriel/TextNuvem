using Microsoft.AspNetCore.Components;
using RestSharp;
using TextNuvem.Commum;
using TextNuvem.Configuration;
using TextNuvem.Dtos.Customers;

namespace TextNuvem.Service;

internal class AuthService: DelegatingHandler
{
    private readonly NavigationManager _navigation;
    private readonly LocalStorageService _storageService;

    public AuthService(LocalStorageService storageService, NavigationManager navigation)
    {
        _storageService = storageService;
        _navigation = navigation;
    }
    
    public async Task<string?> GetToken()
    {
        if (!(await CustomerIsAuthentication()))
        {
            _storageService.RemoveAuthCustomerFromStorage();
            _navigation.NavigateTo("/Customer/Login");
        }

        var expiredToken =await _storageService.GetTokenExpired();
        if (expiredToken < DateTime.UtcNow)
        {
            var expiredRefreshToken =   await _storageService.GetRefreshTokenExpired();
            if (expiredRefreshToken < DateTime.UtcNow)
            {
                _storageService.RemoveAuthCustomerFromStorage();
                _navigation.NavigateTo("/Customer/Login");
            }

            var result = await TryAuthenticationUserRefreshToken();
            if (!result)
            {
                _storageService.RemoveAuthCustomerFromStorage();
                _navigation.NavigateTo("/Customer/Login");
            }
        }

        return await _storageService.GetToken();
    }
    
    
    private async Task<bool> TryAuthenticationUserRefreshToken()
    {
        var result = await LoginRefreshToken();
        if (!result.IsSuccess)
            return false;

        return true;
    }
    
    public async Task<bool> CustomerIsAuthentication()
    {
        var token = await _storageService.GetToken();
        if (token is null)
            return false;
        
        var refreshToken = await _storageService.GetRefreshToken();
        if(refreshToken is null)
            return false;

        return await _storageService.GetRefreshTokenExpired() > DateTime.UtcNow ||
               await _storageService.GetTokenExpired() > DateTime.UtcNow;
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
        Console.WriteLine("Chegou");
        var request = new RestRequest($"{ApiConfiguration.BaseUrl}/Customer/Register",Method.Post)
            .AddJsonBody(model);
        
        var client = new RestClient();
        var response = await client.ExecuteAsync<AuthCustomerResponse>(request);
        if (!response.IsSuccessStatusCode)
            return Result.Failure(response.ErrorMessage!);
    
        await _storageService.AddAuthCustomerInStorage(response.Data!);
        
        return Result.Success();
    }
    
    public async Task<Result> LoginRefreshToken()
    {
        var refreshToken = await _storageService.GetRefreshToken();
        if (refreshToken is null)
            return Result.Failure("Not Found Refresh Token");
        
        var customerId = await _storageService.GetCustomerId();
        if (customerId is null)
            return Result.Failure("Not Found customerId");
        
        var model = new AuthRefreshToken(refreshToken,Guid.Parse(customerId) );
        
        var request = new RestRequest($"{ApiConfiguration.BaseUrl}/Customer/RefreshToken",Method.Post)
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