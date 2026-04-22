namespace TextNuvem.Dtos.Projects;

public record ProjectDto(Guid Id, string Name, DateTime LastUpdate,bool Favorite);