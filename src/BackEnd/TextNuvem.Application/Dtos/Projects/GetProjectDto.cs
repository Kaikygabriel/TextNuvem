using System.Security.AccessControl;

namespace TextNuvem.Application.Dtos.Projects;

public record GetProjectDto(Guid Id,string Name,DateTime LastUpdate,string ContentFolders);