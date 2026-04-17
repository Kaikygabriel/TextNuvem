using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Folder : Entity , IEquatable<Folder>
{
    public Folder() { }

    public Folder(string path,  Guid projectId)
    {
        Id = Guid.NewGuid();
        Path = path;
        ProjectId = projectId;
    }
    
    public Folder(string path, Guid projectId,IEnumerable<Folder>folders,IEnumerable<File>files)
    {
        Id = Guid.NewGuid();
        Path = path;
        ProjectId = projectId;
        
        Folders = folders.ToList();
        Files = files.ToList();
    }
    
    public string Path { get;private set; }
    public Guid ProjectId { get; private init; }
    
    public List<Folder> Folders { get; private  set; } = [];
    public List<File>Files { get; private  set; } = [];
    
    public bool Equals(Folder? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path && Files.SequenceEqual(other.Files);
    }
} 