namespace TextNuvem.Domain.BackOffice.Abstraction;

public abstract class Entity
{
    public Guid Id { get; init; }
} // Project( title ,Folders, id, customerId) -> Folders(Files*,Folder? ,Path,id,projectId) -> Files(Folder ,Name , Content,id) 