namespace TextNuvem.Dtos;

public sealed class File 
{
    public File() { }

    public File(string name, string content,Guid folderId)
    {
        Name = name;
        Content = content;
        FolderId = folderId;
    }

    public string Name { get;private set; }
    public string Content { get; private set; }
    
    public Guid FolderId { get;private  init; }
    
}