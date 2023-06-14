using Moq;
using Test101.Models;

namespace Test101;
public class Moq
{

    [Fact]
    public void Add_AddProduct_ReturnsSameProduct()
    {
        // Arrange
        var dbMock = new DbServiceMock
        { ProcessResult = true };
        ShoppingCart shoppingCart = new(dbMock);

        // Act
        var product = new Product(1, "Shoes", 200);
        var result = shoppingCart.AddProduct(product);

        // Assert
        Assert.True(result);
        Assert.Equal(product, dbMock.ProductBeingProcessed);
    }

    [Fact]
    public void Add_AddInvalidProduct_ReturnsFailure()
    {
        // Arrange
        var dbMock = new DbServiceMock
        { ProcessResult = true };
        ShoppingCart shoppingCart = new(dbMock);

        // Act
        var product = new Product(-1, "Car", 1000);
        var result = shoppingCart.AddProduct(product);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Add_AddProduct_ReturnsSuccess()
    {
        // Arrange    
        Mock<IDbService> _dbServiceMock = new();
        var product = new Product(1, "Shoes", 200);
        _dbServiceMock.Setup(x => x.SaveShoppingCartItem(product)).Returns(true);
        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(product);

        // Assert
        Assert.True(result);

        // Verify!
        _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void Add_AddNullProduct_ReturnsFailure()
    {
        // Arrange
        Mock<IDbService> _dbServiceMock = new();
        _dbServiceMock.Setup(x => x.SaveShoppingCartItem(It.IsAny<Product>())).Returns(true);
        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(It.IsAny<Product>());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Add_AddNullProductV2_ReturnsFailure()
    {
        // Arrange
        Mock<IDbService> _dbServiceMock = new();
        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(null);

        // Assert
        Assert.False(result);

        //Verify
        _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public void Remove_RemoveProduct_ReturnsSuccess()
    {
        // Arrange
        Mock<IDbService> _dbServiceMock = new();
        var product = new Product(1, "Shoes", 200);
        _dbServiceMock.Setup(x => x.RemoveShoppingCartItem(product.Id)).Returns(true);

        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.DeleteProduct(product.Id);

        // Assert
        Assert.True(result);

        //Verify
        _dbServiceMock.Verify(x => x.RemoveShoppingCartItem(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void Remove_RemoveNullProduct_ReturnsFailure()
    {
        // Arrange
        Mock<IDbService> _dbServiceMock = new();
        _dbServiceMock.Setup(x => x.RemoveShoppingCartItem(null)).Returns(false);

        ShoppingCart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.DeleteProduct(null);

        // Assert
        Assert.False(result);
        _dbServiceMock.Verify(x => x.RemoveShoppingCartItem(null), Times.Never);
    }
}