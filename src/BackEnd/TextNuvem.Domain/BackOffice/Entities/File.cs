using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class File : Entity, IEquatable<File>
{
    public File() { }

    public File(string name, string content,Guid folderId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Content = content;
        FolderId = folderId;
    }

    public string Name { get;private set; }
    public string Content { get; private set; }
    
    public Folder Folder { get;  private  init; }
    public Guid FolderId { get;private  init; }

    public bool Equals(File? other)
    {
        if (other is null) return false;
        return other.GetHashCode() == GetHashCode() && Folder.Equals(other.Folder);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Content);
    }
}