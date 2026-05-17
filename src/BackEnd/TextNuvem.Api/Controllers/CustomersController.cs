using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextNuvem.Application.Dtos.Customers;
using TextNuvem.Application.UseCases.Customers.Command.Request;
using TextNuvem.Application.UseCases.Customers.Command.Response;
using TextNuvem.Application.UseCases.Customers.Query.Request;

namespace TextNuvem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ISender _sender;

    public CustomerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("RefreshToken")]
    [AllowAnonymous]
    [Produces<AuthCustomerResponse>]
    public async Task<ActionResult> RefreshToken(LoginByRefreshTokenRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Register")]
    [AllowAnonymous]
    [Produces<AuthCustomerResponse>]
    public async Task<ActionResult> Register(RegisterCustomerRequest request ,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Login")]
    [AllowAnonymous]
    [Produces<AuthCustomerResponse>]
    public async Task<ActionResult> Login(LoginCustomerRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [Authorize]
    [HttpGet("DashBoard")]
    [Produces<CustomerDashBoard>]
    public async Task<ActionResult> DashBoard([FromQuery]GetCustomerDashBoardRequest request,CancellationToken cancellationToken)
    {
        if (request.CustomerId.ToString() != User.Identity!.Name)
            return Forbid();
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [Authorize]
    [HttpGet("Last-Project")]
    public async Task<ActionResult> LastProject([FromQuery]GetLastProjectUpdateRequest request)
    {
        if (request.CustomerId.ToString() != User.Identity!.Name)
            return Forbid();
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
