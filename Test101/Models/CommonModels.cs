namespace Test101.Models;

public interface IDbService
{
    bool SaveShoppingCartItem(Product prod);
    bool RemoveShoppingCartItem(int? id);
}
public class DbServiceMock : IDbService
{
    public bool ProcessResult { get; set; }
    public Product? ProductBeingProcessed { get; set; }
    public int ProductIdBeingProcessed { get; set; }

    public bool RemoveShoppingCartItem(int? id)
    {
        if (id != null)
            ProductIdBeingProcessed = Convert.ToInt32(id);
        return ProcessResult;
    }
    public bool SaveShoppingCartItem(Product prod)
    {
        ProductBeingProcessed = prod;
        return ProcessResult;
    }
}

public record Product(int Id, string Name, double price);
public class ShoppingCart
{
    private IDbService _dbService;

    public ShoppingCart(IDbService dbService)
    {
        _dbService = dbService;
    }

    public bool AddProduct(Product? product)
    {
        if (product == null)
            return false;

        if (product.Id <= 0)
            return false;

        return _dbService.SaveShoppingCartItem(product);
    }
    public bool DeleteProduct(int? id)
    {
        if (id == null)
            return false;

        if (id == 0)
            return false;

        _dbService.RemoveShoppingCartItem(id);
        return true;
    }
}