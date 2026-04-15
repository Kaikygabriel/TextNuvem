using System.ComponentModel.DataAnnotations;
using TextNuvem.Domain.BackOffice.Entities;
using File = TextNuvem.Domain.BackOffice.Entities.File;

namespace TextNuvem.Application.Dtos;

public record FolderDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Path { get; init; } = "";
    
    public Guid? FolderParentId { get; init; }
    public Guid ProjectId { get; private init; }

    public List<FolderDto> Folders { get; set; } = [];
    public List<File>Files { get;set; } = [];
    
    public static implicit operator Folder(FolderDto folderDto)
    {
        if(folderDto.FolderParentId is not null)
            return new Folder(folderDto.Path, folderDto.ProjectId,(Guid)folderDto.FolderParentId);
        
        return new Folder(folderDto.Path, folderDto.ProjectId);
    }

    public static List<Folder> ToFolder(IEnumerable<FolderDto> folders)
        => folders.Select(x => (Folder)x).ToList();
}