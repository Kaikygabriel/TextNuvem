using Microsoft.AspNetCore.Mvc;
using TextNuvem.Application.Services;
using TextNuvem.Application.UseCases.Project.Command.Request;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.Repositories;
using TextNuvem.Infra.Dtos.Folders;

namespace TextNuvem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("Test")]
    public async Task<ActionResult> Test(ICompactorService services,IProjectRepository projectRepostiory)
    {
        var project = await projectRepostiory.GetById(new Guid("e71d026d-c899-470e-84b9-2695d96e8749"));
        var folders = new List<Folder>();
        
        var projectId = project.Id;

        var list = new List<Folder>();

        var rootFolder = new Folder("src", projectId);
        
        var controllersFolder = new Folder("src/controllers", projectId);
        
        var homeController = new Domain.BackOffice.Entities.File(
            "HomeController.cs",
            "public class HomeController {}",
            controllersFolder.Id
        );

        var programFile = new Domain.BackOffice.Entities.File(
            "Program.cs",
            "Console.WriteLine(\"Hello World\");",
            controllersFolder.Id
        );

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