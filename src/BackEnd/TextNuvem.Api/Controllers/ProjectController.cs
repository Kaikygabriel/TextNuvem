using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Application.UseCases.Project.Query.Request;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Dtos.Folders;
using File = TextNuvem.Domain.BackOffice.Entities.File;

namespace TextNuvem.Api.Controllers;

[ApiController]
//[Authorize]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ISender _sender;

    public ProjectController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery]GetProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPost("Create")]
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
    public async Task<ActionResult> Remove(RemoveProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}