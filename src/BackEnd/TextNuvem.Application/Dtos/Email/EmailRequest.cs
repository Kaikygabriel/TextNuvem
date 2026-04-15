namespace TextNuvem.Application.Dtos;

public record EmailRequest(string To,string ToName,string Subject,string Body);