using TextNuvem.Domain.BackOffice.Entities;

namespace TextNuvem.Application.Dtos.Projects;

public record ProjectDto(Guid Id, string Name, DateTime LastUpdate)
{
    public static List<ProjectDto> ToProjectDtos(List<Project> projects)
        => projects.Select(x => (ProjectDto)x).OrderBy(x=>x.LastUpdate).ToList();
    public static explicit operator ProjectDto(Project project)
        => new(project.Id, project.Name, project.LastUpdate);
};