using File = TextNuvem.Domain.BackOffice.Entities.File;

namespace TextNuvem.Infra.Dtos;

public record FileDatabaseDto(string Name,string Content,Guid FolderId)
{
    public static List<File> ToFile(IEnumerable<FileDatabaseDto> filesDtos)
        => filesDtos.Select(x => (File)x).ToList();
    
    public static implicit operator File(FileDatabaseDto file)
        => new(file.Name, file.Content, file.FolderId);
}