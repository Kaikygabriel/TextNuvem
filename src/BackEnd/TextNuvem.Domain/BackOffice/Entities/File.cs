using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class File : Entity
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

    public string Name { get; private init; }
    public string Content { get; private init; }
    public Folder Folder { get; private init; }
    
}