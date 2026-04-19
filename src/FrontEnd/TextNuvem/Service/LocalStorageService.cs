using Microsoft.JSInterop;
using TextNuvem.Configuration;
using TextNuvem.Dtos.Customers;

namespace TextNuvem.Service;

public sealed class LocalStorageService
{
     private readonly IJSRuntime _js;

     public LocalStorageService(IJSRuntime js)
     {
          _js = js;
     }
     
     public async Task AddAuthCustomerInStorage(AuthCustomerResponse response,string email)
     {
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Token_Key, response.Token);
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Customer_Id, response.CustomerId);
          await _js.InvokeVoidAsync("localStorage.setItem","Email",email);

          var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
     }

     public async Task<string?> GetToken()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem",AuthConfiguration.Token_Key);
     
     public async Task<string?> GetCustomerId()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem",AuthConfiguration.Customer_Id);
     
     public async Task<string?> GetEmail()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem","Email");

}