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

    public string Name { get;private set; }
    public Customer Customer { get; private init; }
    public Guid CustomerId { get; private init; }
    public List<Folder> Folders { get;private set; } = [];

    public void UpdateFolders(List<Folder>folders)
    {
        foreach (var folder in folders)
        {
            var folderExists = Folders.FirstOrDefault(x => x.Id == folder.Id);
            if (folderExists is not null)
            {
                //Verificar se a pasta sofreu alteração atravez do equals personalizado !!
            }
            Folders.Add(folder);
        }
    }
}