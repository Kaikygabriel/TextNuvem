namespace TextNuvem.Dtos.Customers;

public class CustomerLoginDto
{
    public CustomerLoginDto()
    {
        
    }
    public CustomerLoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; } 
    public string Password { get; set; }

    
}