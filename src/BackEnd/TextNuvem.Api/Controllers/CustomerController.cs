using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextNuvem.Application.UseCases.Customers.Command.Request;
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
    public async Task<ActionResult> RefreshToken(LoginByRefreshTokenRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterCustomerRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginCustomerRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [Authorize]
    [HttpGet("DashBoard")]
    public async Task<ActionResult> DashBoard([FromQuery]GetCustomerDashBoardRequest request)
    {
        if (request.CustomerId.ToString() != User.Identity!.Name)
            return Forbid();
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [Authorize]
    [HttpGet("LastProject")]
    public async Task<ActionResult> LastProject([FromQuery]GetLastProjectUpdateRequest request)
    {
        if (request.CustomerId.ToString() != User.Identity!.Name)
            return Forbid();
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}