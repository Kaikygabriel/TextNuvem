using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Customers.Command.Request;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Application.UseCases.Customers.Command.Handler;

internal sealed class LoginByRefreshTokenHandler  : IRequestHandler<LoginByRefreshTokenRequest,Result<AuthCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginByRefreshTokenHandler(ICustomerRepository customerRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthCustomerResponse>> Handle(LoginByRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var customer =await _customerRepository.GetById(request.CustomerId);
        if (customer is null )
            return new Error("Email or Password Invalid");
        
        if(customer.RefreshToken is null)
            return new Error("Token is invalid");
        
        var resultVerifyToken = customer.RefreshToken.VerifyRefreshToken(request.Token);
        if (!resultVerifyToken.IsSuccess)
            return resultVerifyToken.Error;

        var token = _tokenService.GenerateAccessToken(customer);
        var refreshToken = new RefreshToken(_tokenService.GenerateRefreshToken());
        
        customer.SetRefreshToken(refreshToken);
        
        _customerRepository.Update(customer);
        await _unitOfWork.CommitAsync(); 
        
        var response = new AuthCustomerResponse(token, refreshToken.Token, customer.Id);
        return response;
    }
}