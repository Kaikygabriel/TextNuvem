using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Domain.BackOffice.ValueObject;

public class Email
{
    private Email()
    {
        
    }
    private Email(string address)
        => Address = address;
    

    public string Address { get;private set; }

    private static bool AddressIsInvalid(string address)
        => string.IsNullOrWhiteSpace(address) ||
           !address.Contains('@') ||
           address.Split('@').Length != 2 ||
           address.Split('@')[1].Length <= 2 ||
           address.Split('@')[0].Length <= 2;

    public static class Factory 
    {
        public static Result<Email> Create(string address)
        {
            if (AddressIsInvalid(address))
                return new Error("Address in Email Is Invalid");
            return new Email(address);
        }
    }
}