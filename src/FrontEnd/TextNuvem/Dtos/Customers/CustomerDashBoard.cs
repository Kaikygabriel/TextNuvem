using TextNuvem.Dtos.Projects;

namespace TextNuvem.Dtos.Customers;

public record CustomerDashBoard(Guid Id,IEnumerable<ProjectDto>Projects,string Name,string Email);