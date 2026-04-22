using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Domain.BackOffice.ValueObject;

namespace TextNuvem.Test.Domain.BackOffice.Entity;

public class ProjectTest
{
    private static Customer Customer = new Customer(Email.Factory.Create("teste@gmail.com").Value,
        Password.Factory.Create("teste@123").Value, "teste");

    private readonly Project Project = new Project("teste", Customer);

    [Fact]
    public void Set_Project_As_Favorite_Should_Return_True()
    {
        Project.SetFavorite();
        Assert.True(Project.IsFavorite);
    }
    
    [Fact]
    public void Remove_Project_As_Favorite_Should_Return_False()
    {
        Project.RemoveFavorite();
        Assert.False(Project.IsFavorite);
    }
    
}