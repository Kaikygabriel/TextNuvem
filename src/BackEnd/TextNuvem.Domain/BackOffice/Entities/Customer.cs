using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.Commum;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Domain.BackOffice.Entities;

public sealed class Customer: Entity
{
    private Customer()
    {
        
    }
    public Customer(Email email, Password password, string name)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        Name = name;
    }

    public Email Email { get;private set; }
    public Password Password { get;private set; }
    public string Name { get; private init; }
    
    public RefreshToken? RefreshToken { get; private set; }
    
    public Project? LastProjectUpdate { get;private set; }
    public Guid? LastProjectIdUpdate { get;private set; }
    
    public List<Project> Projects { get; private set; } = [];
    public List<ChangesDate> ChangesDate { get; private set; } = [];

    public Result AddProject(Project project)
    {
        if (Projects.Exists(x => x.Name == project.Name))
            return new Error("Project already exists!");
        
        Projects.Add(project);
        ChangeUpdate();
        
        return Result.Success();
    }
    
    public Result RemoveProject(Guid id)
    {
        if (!Projects.Exists(x => x.Id ==id))
            return new Error("Project not found !");
        
        Projects.RemoveAll(x=>x.Id == id);
        ChangeUpdate();
        
        return Result.Success();
    }

    public void UpdateLastProject(Project projectLastUpdate)
    {
        ChangeUpdate();
        
        LastProjectUpdate = projectLastUpdate;
        LastProjectIdUpdate = projectLastUpdate.Id;
    }
    
    public void SetRefreshToken(RefreshToken token)
        => RefreshToken = token;

    private void ChangeUpdate()
    {
        var changeDate = ChangesDate.FirstOrDefault(x => x.Date.Date == DateTime.UtcNow.Date);
        
        if (changeDate is not null)
        {
            changeDate.AddQuantity(1);
            return;
        }

        var newChangeDate = ValueObject.ChangesDate.Factory.Create();
        ChangesDate.Add(newChangeDate);
    }
}