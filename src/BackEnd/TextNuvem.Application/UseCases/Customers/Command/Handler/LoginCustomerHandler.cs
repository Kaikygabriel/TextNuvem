using MediatR;
using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Customers.Command.Request;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Application.UseCases.Customers.Command.Handler;

internal sealed class LoginCustomerHandler : IRequestHandler<LoginCustomerRequest,Result<AuthCustomerResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITokenService _tokenService;

    public LoginCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthCustomerResponse>> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByEmail(request.Email);
        if (customer is null || !customer.Password.Verify(request.Password))
            return new Error("Email or Password is Invalid");

        var token = _tokenService.GenerateAccessToken(customer);
        var refreshToken = new RefreshToken(_tokenService.GenerateRefreshToken());
        
        customer.SetRefreshToken(refreshToken);

        var response = new AuthCustomerResponse(token, refreshToken.Token, customer.Id);

        _customerRepository.Update(customer);
        
        await _unitOfWork.CommitAsync();
        return response;
    }
}