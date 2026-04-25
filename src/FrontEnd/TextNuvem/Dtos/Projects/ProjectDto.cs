namespace TextNuvem.Dtos.Projects;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Favorite { get; set; }

    public ProjectDto(Guid id, string name, DateTime lastUpdate, bool favorite)
    {
        Id = id;
        Name = name;
        LastUpdate = lastUpdate;
        Favorite = favorite;
    }

    public ProjectDto()
    {
        
    }
}