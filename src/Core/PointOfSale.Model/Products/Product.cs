using PointOfSale.Model.Categories;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Primitives;
using PointOfSale.Model.Sales;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Model.Products;

public class Product
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public Money Cost { get; private set; }
    public int Stock { get; private set; }
    public MarkupPolicy MarkupPolicy { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public BranchId BranchId { get; private set; }

    protected Product() { }

    private Product(string name, Money price, Money cost, int stock, CategoryId categoryId,
        MarkupPolicy markupPolicy, SupplierId supplierId, BranchId branchId)
    {
        Id = new ProductId(Guid.NewGuid());
        Name = name;
        Price = price;
        Cost = cost;
        Stock = stock;
        CategoryId = categoryId;
        MarkupPolicy = markupPolicy;
        SupplierId = supplierId;
        BranchId = branchId;
    }

    public static Result<Product> Create(string name, Money price, Money cost, int stock, CategoryId categoryId,
        MarkupPolicy markupPolicy,  SupplierId supplierId, BranchId branchId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Product>.Failure("Invalid product name or image url.");
        }

        if (stock <= 0)
        {
            return Result<Product>.Failure("Stock cannot be negative or zero.");
        }

        var product = new Product(name, price, cost, stock, categoryId, markupPolicy, supplierId, branchId);
        return Result<Product>.Success(product);
    }

    public Result Update(string name, Money price, Money cost, int stock, CategoryId categoryId,
        MarkupPolicy markupPolicy,  SupplierId supplierId, BranchId branchId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure("Name cannot be empty.");
        }

        if (stock <= 0)
        {
            return Result.Failure("Stock cannot be negative or zero.");
        }

        Name = name;
        Price = price;
        Cost = cost;
        Stock = stock;
        CategoryId = categoryId;
        MarkupPolicy = markupPolicy;
        SupplierId = supplierId;
        BranchId = branchId;

        return Result.Success();
    }

    public Result AddStock(int newStock)
    {
        if (newStock <= 0)
        {
            return Result.Failure("Stock to add must be greater than zero.");
        }

        Stock += newStock;
        return Result.Success();
    }

    public Result DecreaseStock(int decreaseStock)
    {
        if (decreaseStock <= 0)
        {
            return Result.Failure("Stock to decrease must be greater than zero.");
        }

        if (Stock - decreaseStock < 0)
        {
            return Result.Failure("Stock cannot be negative after decrease.");
        }

        Stock -= decreaseStock;
        return Result.Success();
    }
}
