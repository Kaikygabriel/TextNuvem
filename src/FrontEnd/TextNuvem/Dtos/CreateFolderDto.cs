using System.ComponentModel.DataAnnotations;

namespace TextNuvem.Dtos;

public class CreateFolderDto
{
    [Required]
    public string Name { get; set; }
}