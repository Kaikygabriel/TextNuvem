using MediatR;
using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Customers.Command.Request;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Application.UseCases.Customers.Command.Handler;

internal sealed class RegisterCustomerHandler : IRequestHandler<RegisterCustomerRequest,Result<AuthCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public RegisterCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthCustomerResponse>> Handle(RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        if (await _customerRepository.Exists(request.Email))
            return new Error("Email in use !");

        var resultCreateCustomer = request.ToEntity();
        if (!resultCreateCustomer.IsSuccess)
            return resultCreateCustomer.Error;

        var customer = resultCreateCustomer.Value;

        var token = _tokenService.GenerateAccessToken(customer);
        var refreshToken = new RefreshToken(_tokenService.GenerateRefreshToken());
        
        customer.SetRefreshToken(refreshToken);

        _customerRepository.Create(customer);
        await _unitOfWork.CommitAsync();
        
        var response = new AuthCustomerResponse(token, refreshToken.Token, customer.Id);
        return response;
    }
}