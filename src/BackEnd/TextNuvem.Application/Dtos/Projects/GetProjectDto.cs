using System.Security.AccessControl;

namespace TextNuvem.Application.Dtos.Projects;

public record GetProjectDto(string Name,DateTime LastUpdate,string ContentFolders);