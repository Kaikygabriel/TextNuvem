using TextNuvem.Application.Dtos.Projects;

namespace TextNuvem.Application.Dtos.Customers;

public record CustomerDashBoard(Guid Id,IEnumerable<ProjectDto>Projects,string Name,string Email);