using TextNuvem.Application.Dtos.Projects;
using TextNuvem.Application.Dtos.ValueObjects;

namespace TextNuvem.Application.Dtos.Customers;

public record CustomerDashBoard(Guid Id,IEnumerable<ProjectDto>Projects,string Name,string Email,IEnumerable<ChangesDateDto>ChangesDateDtos);