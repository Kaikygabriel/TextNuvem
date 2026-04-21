using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Test.Domain.BackOffice.ValueObject;

public class PasswordTest
{
    private const string Password_Valid = "teste@123";
    private const string Password_Invalid_Empty = "";
    private const string Password_Invalid_Small = "123";

    [Fact]
    public void Create_Password_Should_Return_False_When_Password_Is_Small()
    {
        var result = Password.Factory.Create(Password_Invalid_Small);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Password_Should_Return_False_When_Password_Is_Empty()
    {
        var result = Password.Factory.Create(Password_Invalid_Empty);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public void Create_Password_Should_Return_True_WHen_Password_Is_Valid()
    {
        var result = Password.Factory.Create(Password_Valid);
        Assert.True(result.IsSuccess);
    }
}