using System.ComponentModel.DataAnnotations;

namespace TextNuvem.Dtos;

public sealed class NameFileDto 
{
    [Required]
    [Length(minimumLength:3,maximumLength:180)]
    public string Name { get; set; }
}