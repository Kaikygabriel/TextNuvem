namespace TextNuvem.Service;

public class AuthService: DelegatingHandler
{
    private readonly LocalStorageService _storageService;

    public AuthService(LocalStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _storageService.GetToken();
          
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
          
        return await base.SendAsync(request, cancellationToken);
    } 
    public async Task<bool> CustomerIsAuthentication()
    {
        return await _storageService.GetToken() is not null;
    }
}