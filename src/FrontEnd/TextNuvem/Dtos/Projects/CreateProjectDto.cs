namespace TextNuvem.Dtos.Projects;

public class CreateProjectDto
{
    public CreateProjectDto()
    {
        
    }

    public string name { get; set; }
    public Guid customerId { get; set; }
}