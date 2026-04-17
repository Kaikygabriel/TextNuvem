using MediatR;
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
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ISender _sender;

    public ProjectController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Created() : BadRequest(result.Error);
    }
    
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery]GetProjectRequest request)
    {
        var result = await _sender.Send(request);
       
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    [HttpPut("Update/Files")]
    public async Task<ActionResult> UpdateFiles(UpdateFilesInProjectRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
    
    [HttpGet("Test")]
    public async Task<ActionResult> Test(ICompactorService services,IProjectRepository projectRepostiory)
    {
        var project = await projectRepostiory.GetById(new Guid("e71d026d-c899-470e-84b9-2695d96e8749"));
        var folders = new List<Folder>();
        
        var projectId = project.Id;

        var list = new List<Folder>();
        
        // ROOT
        var rootFolder = new Folder("src", projectId);
        
        // SUBPASTA
        var controllersFolder = new Folder("src/controllers", projectId);

        // ⚠️ IMPORTANTE: evitar loop (Folder dentro de File)
        var homeController = new File(
            "HomeController.cs",
            "public class HomeController {}",
            controllersFolder.Id
        );

        var programFile = new File(
            "Program.cs",
            "Console.WriteLine(\"Hello World\");",
            controllersFolder.Id
        );

        // Montando estrutura
        controllersFolder.Files.Add(homeController);
        rootFolder.Folders.Add(controllersFolder);
        rootFolder.Files.Add(programFile);
        
        list.Add(rootFolder);
        var result = services.CompressObject(list);
        Console.WriteLine(result);
        return Ok(new UpdateFilesInProjectRequest(result, project.Id));
    }
    
    [HttpPost("Test")]
    public async Task<ActionResult> TestCreate(ICompactorService service,string dados)
    {
        var result = service.DecompressObject<List<FolderDatabaseDto>>(dados).Select(x=>(Folder)x);
        return Ok(result);
    }
}