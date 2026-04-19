using System.ComponentModel.DataAnnotations;

namespace TextNuvem.Dtos.Folder;

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
    

}