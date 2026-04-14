using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class File : Entity, IEquatable<File>
{
    private File()
    {
        
    }
    public File(string name, string content, Folder folder)
    {
        Name = name;
        Content = content;
        Folder = folder;
    }

    public string Name { get; private set; }
    public string Content { get; private set; }
    public Folder Folder { get; private set; }

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