using Microsoft.JSInterop;
using TextNuvem.Configuration;
using TextNuvem.Dtos.Customers;

namespace TextNuvem.Service;

internal sealed class LocalStorageService
{
     private readonly IJSRuntime _js;

     public LocalStorageService(IJSRuntime js)
     {
          _js = js;
     }
     
     public async Task RemoveAuthCustomerFromStorage()
     {
          await _js.InvokeVoidAsync("localStorage.removeItem", AuthConfiguration.Token_Key);
          await _js.InvokeVoidAsync("localStorage.removeItem", AuthConfiguration.Refresh_Token);
          await _js.InvokeVoidAsync("localStorage.removeItem", AuthConfiguration.Customer_Id);
          await _js.InvokeVoidAsync("localStorage.removeItem", AuthConfiguration.Expired_Token);
          await _js.InvokeVoidAsync("localStorage.removeItem", AuthConfiguration.Expired_RefreshToken);
          await _js.InvokeVoidAsync("localStorage.removeItem", "Email");
     }
     
     public async Task AddAuthCustomerInStorage(AuthCustomerResponse response)
     {
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Token_Key, response.Token);
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Refresh_Token, response.RefreshToken);
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Customer_Id, response.CustomerId);
          
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Expired_Token, 
               DateTime.UtcNow.AddHours(8));
          await _js.InvokeVoidAsync("localStorage.setItem",AuthConfiguration.Expired_RefreshToken, 
               DateTime.UtcNow.AddHours(16));

          var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
     }
     
     public async Task<DateTime?> GetTokenExpired()
     {
          try
          {
               return await _js.InvokeAsync<DateTime>("localStorage.getItem",AuthConfiguration.Expired_Token);
          }
          catch (Exception e)
          {
               return null;
          }
     } 
          
     
     public async Task<DateTime?> GetRefreshTokenExpired()
     {
          try
          {
               return await _js.InvokeAsync<DateTime>("localStorage.getItem",AuthConfiguration.Expired_RefreshToken);
          }
          catch (Exception e)
          {
               return null;
          }
     } 
     public async Task<string?> GetToken()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem",AuthConfiguration.Token_Key);
     
     public async Task<string?> GetRefreshToken()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem",AuthConfiguration.Refresh_Token);
     
     public async Task<string?> GetCustomerId()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem",AuthConfiguration.Customer_Id);
     
     public async Task<string?> GetEmail()
          =>  await _js.InvokeAsync<string?>("localStorage.getItem","Email");

}