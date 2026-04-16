using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Project :Entity
{
    private Project()
    {
        
    }
    public Project(string name, Customer customer, Guid customerId)
    {
        Name = name;
        Customer = customer;
        CustomerId = customerId;
    }

    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public string Name { get;private set; }
    public Customer Customer { get; private init; }
    public Guid CustomerId { get; private init; }
    public List<Folder> Folders { get;private set; } = [];//TEXT -> compress ->  Json -> List<Folder>

    public void UpdateFolders(List<Folder>folders)
    {
        foreach (var folder in folders)
        {
            var folderExists = Folders.FirstOrDefault(x => x.Id == folder.Id);
            
            if (folderExists is not null) 
                Folders[Folders.IndexOf(folderExists)] = folder;
            else
                Folders.Add(folder);
        }

        UpdateDate();
    }

    private void UpdateDate()
        => LastUpdate = DateTime.UtcNow;
}