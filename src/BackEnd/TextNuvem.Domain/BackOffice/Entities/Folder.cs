using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Folder : Entity , IEquatable<Folder>
{
    private Folder()
    {
        
    }
    public Folder(string path,  Guid projectId)
    {
        Path = path;
        ProjectId = projectId;
    }
    
    public Folder(string path, Guid projectId,Guid folderParentId)
    {
        Path = path;
        ProjectId = projectId;
        
        FolderParentId = folderParentId;
    }
    
    public string Path { get;private set; }
    public Project Project { get;private init; }
    public Guid ProjectId { get; private init; }
    
    
    public Folder? FolderParent { get;private init; }
    public Guid? FolderParentId { get;private init; }


    public List<Folder> Folders { get; private set; } = [];
    public List<File>Files { get;private  set; } = [];
    
    public bool Equals(Folder? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path && Files.SequenceEqual(other.Files);
    }
} 