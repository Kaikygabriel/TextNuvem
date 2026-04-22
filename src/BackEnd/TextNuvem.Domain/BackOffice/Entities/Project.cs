using TextNuvem.Domain.BackOffice.Abstraction;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Project :Entity
{
    private Project()
    {
        
    }
    public Project(string name, Customer customer)
    {
        Id = Guid.NewGuid();
        Name = name;
        Customer = customer;
        CustomerId = customer.Id;
    }

    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public string Name { get;private set; }
    public Customer Customer { get; private init; }
    public Guid CustomerId { get; private init; }
    public List<Folder> Folders { get;private set; } = [];
    public bool IsFavorite { get;private set; }

    public void SetFavorite()
        => IsFavorite = true;
    public void RemoveFavorite()
        => IsFavorite = false;
    
    public void UpdateFolders(List<Folder>folders)
    {
        if (Folders is null ||!Folders.Any())
        {
            Folders = folders;
            return;
        }

        Folders = folders;

        UpdateDate();
    }

    private void UpdateDate()
        => LastUpdate = DateTime.UtcNow;
}