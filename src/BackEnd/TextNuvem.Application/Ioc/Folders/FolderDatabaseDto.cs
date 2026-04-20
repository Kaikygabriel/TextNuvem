using TextNuvem.Application.Ioc.Files;
using TextNuvem.Domain.BackOffice.Entities;
using File = TextNuvem.Domain.BackOffice.Entities.File;

namespace TextNuvem.Application.Ioc.Folders;

public record FolderDatabaseDto(Guid ProjectId,string Path,List<FolderDatabaseDto>Folders,List<FileDatabaseDto>Files)
{
    public static implicit operator Folder(FolderDatabaseDto folderDto)
        => new Folder(
            folderDto.Path,
            folderDto.ProjectId,
            folderDto.Folders.Select(x=> (Folder)x),
            folderDto.Files.Select(x=>(File)x));
    
  
}