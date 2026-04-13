namespace TextNuvem.Domain.BackOffice.Abstraction;

public abstract class Entity
{
    public Guid Id { get; private init; } = Guid.NewGuid();
} // Project( title ,Folders , files , id, customerId) -> Folders(Files, Path,id,projectId) -> Files(project, Folder? ,Name , Content, Extension,id) 