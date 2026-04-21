using TextNuvem.Domain.BackOffice.Abstraction;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Test.Domain.BackOffice.ValueObject;

public class EmailTest
{
    private const string Address_Valid = "teste@gmail.com";
    private const string Address_Small_Invalid = "as@as";
    private const string Address_No_Arroba_Invalid = "testegmail.com";

    [Fact]
    public void Create_Email_Should_Return_true_When_address_Is_valid()
    {
        var result = Email.Factory.Create(Address_Valid);
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Email_Should_Return_False_When_address_Is_Small()
    {
        var result = Email.Factory.Create(Address_Small_Invalid);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Email_Should_Return_False_When_address_No_Contains_Arroba()
    {
        var result = Email.Factory.Create(Address_No_Arroba_Invalid);
        Assert.False(result.IsSuccess);
    }
}