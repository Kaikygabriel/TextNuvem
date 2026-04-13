using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Domain.BackOffice.ValueObject;

public sealed record Password
{
    private Password()
    {
        
    }
    private Password(string hashPassword)
    {
        HashPassword = CreateHash(hashPassword);
    }

    public string HashPassword { get; private init; }

    private string CreateHash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string otherPassword)
        => BCrypt.Net.BCrypt.Verify(otherPassword, HashPassword);
    
    public static class Factory
    {
        public static Result<Password> Create(string password)
        {
            if (PasswordIsInvalid(password))
                return new Error("Password is Invalid !");
            return new Password(password);
        }
    }

    private static bool PasswordIsInvalid(string password)
        => string.IsNullOrWhiteSpace(password)|| password.Length <= 3;
}