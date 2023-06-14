using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Test101.Models;

namespace Test101;

public class AutoFixture
{
    [Theory]
    [InlineData("fatih", "kucukkara", "fatih kucukkara")]
    public void FullName_CreateCustomer_ReturnsSuccess(string name, string surname, string fullname)
    {
        // Arrange
        Fixture fixture = new();

        var sut = fixture.Build<Customer>()
            .With(n => n.Name, name)
            .With(n => n.Surname, surname)
            .Create();

        // Act
        var actual = sut.FullName;

        // Assert
        Assert.Equal(fullname, actual);
    }

    [Fact]
    public void Add_AddProduct_ReturnsSameProduct()
    {

        Mock<IDbService> _dbServiceMock = new();
        _dbServiceMock.Setup(x => x.SaveShoppingCartItem(It.IsAny<Product>())).Returns(true);
        var _product = new Fixture().Create<Product>();

        // Arrange
        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(_product);

        // Assert
        Assert.True(result);

        //Verify
        _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void Add_AddProductFullFixture_ReturnsSameProduct()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        var dbServiceMock = fixture.Freeze<Mock<IDbService>>();
        dbServiceMock.Setup(x => x.SaveShoppingCartItem(It.IsAny<Product>())).Returns(true);

        var _product = fixture.Create<Product>();

        // Arrange
        ShoppingCart shoppingCart = fixture.Create<ShoppingCart>();

        // Act
        var result = shoppingCart.AddProduct(_product);

        // Assert
        Assert.True(result);
    }

}

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Address { get; set; }
    public string FullName => $"{Name} {Surname}";
}
