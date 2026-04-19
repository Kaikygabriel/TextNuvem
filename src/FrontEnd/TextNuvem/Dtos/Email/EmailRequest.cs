namespace TextNuvem.Dtos.Email;

public record EmailRequest(string To,string ToName,string Subject,string Body);