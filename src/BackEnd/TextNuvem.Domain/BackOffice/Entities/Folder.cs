using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Folder : Entity , IEquatable<Folder>
{
    private Folder()
    {
        
    }
    public Folder(string path, Project project, Guid projectId)
    {
        Path = path;
        Project = project;
        ProjectId = projectId;
    }

    public string Path { get;private set; }
    public List<File>Files { get;private  set; } = [];
    public Project Project { get;private init; }
    public Guid ProjectId { get; private init; }

    public bool Equals(Folder? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path && Files.Equals(other.Files);
    }
} 