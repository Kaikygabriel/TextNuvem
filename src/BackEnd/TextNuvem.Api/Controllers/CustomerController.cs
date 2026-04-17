using MediatR;
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

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterCustomerRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginCustomerRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    
    [HttpGet("DashBoard")]
    public async Task<ActionResult> DashBoard(GetCustomerDashBoardRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}