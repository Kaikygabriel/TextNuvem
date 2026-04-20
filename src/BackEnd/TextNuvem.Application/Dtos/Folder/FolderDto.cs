using System.ComponentModel.DataAnnotations;
using File = TextNuvem.Domain.BackOffice.Entities.File;

namespace TextNuvem.Application.Dtos.Folder;

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
    
    public static implicit operator Domain.BackOffice.Entities.Folder(FolderDto folderDto)
    {
        return new Domain.BackOffice.Entities.Folder(folderDto.Path, folderDto.ProjectId);
    }

    public static List<Domain.BackOffice.Entities.Folder> ToFolder(IEnumerable<FolderDto> folders)
        => folders.Select(x => (Domain.BackOffice.Entities.Folder)x).ToList();
}