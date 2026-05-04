using TextNuvem.Dtos.Projects;
using TextNuvem.Dtos.ValueObjects;

namespace TextNuvem.Dtos.Customers;

public record CustomerDashBoard(Guid Id,IEnumerable<ProjectDto>Projects,string Name,string Email,List<ChangesDateDto>ChangesDateDtos);