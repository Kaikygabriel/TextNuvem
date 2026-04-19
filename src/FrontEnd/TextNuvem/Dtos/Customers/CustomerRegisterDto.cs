namespace TextNuvem.Dtos.Customers;

public class CustomerRegisterDto
{
    public CustomerRegisterDto() 
    {
        
    }

    public CustomerRegisterDto(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
    }
    public string Email { get; set; } 
    public string Password { get; set; }
    public string Name { get; set; } 
    
}