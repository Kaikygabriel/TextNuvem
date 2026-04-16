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
        Email = email;
        Password = password;
        Name = name;
    }

    public Email Email { get;private set; }
    public Password Password { get;private set; }
    public string Name { get; private init; }
    public List<Project> Projects { get; private set; } = [];
    public RefreshToken? RefreshToken { get; private set; }

    public Result AddProject(Project project)
    {
        if (Projects.Exists(x => x.Name == project.Name))
            return new Error("Project already exists!");
        Projects.Add(project);
        return Result.Success();
    }
    
    public Result RemoveProject(Guid id)
    {
        if (!Projects.Exists(x => x.Id ==id))
            return new Error("Project not found !");
        Projects.RemoveAll(x=>x.Id == id);
        return Result.Success();
    }

    public void SetRefreshToken(RefreshToken token)
        => RefreshToken = token;
    
}