using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Application.UseCases.Project.Query.Request;

namespace TextNuvem.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ISender _sender;

    public ProjectController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery]GetProjectRequest request,CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("SetFavorite")]
    public async Task<ActionResult> SetFavorite(SetFavoriteRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    
    [HttpPost("RemoveFavorite")]
    public async Task<ActionResult> RemoveFavorite(RemoveFavoriteRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    
    [HttpPut("Update/Files")]
    public async Task<ActionResult> UpdateFiles(UpdateFilesInProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    [HttpDelete]
    public async Task<ActionResult> Remove( [FromQuery] Guid customerId,
                                               [FromQuery] Guid projectId)
    {
        var request = new RemoveProjectRequest(customerId, projectId);
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}