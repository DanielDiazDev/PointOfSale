using PointOfSale.Model.Categories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Model.Products;

public class ProductBuilder
{
    private string _name = "Name";
    private Money _cost = new Money(3);
    private Money _price = new Money(3);
    private int _stock = 32;
    private CategoryId _categoryId = new CategoryId(Guid.NewGuid());
    private MarkupPolicy _markupPolicy = new MarkupPolicy(true, 3);
    private SupplierId _supplierId = new SupplierId(Guid.NewGuid());
    private BranchId _branchId = new BranchId(Guid.NewGuid());

    public ProductBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProductBuilder WithCost(Money cost)
    {
        _cost = cost;
        return this;
    }

    public ProductBuilder WithPrice(Money price)
    {
        _price = price;
        return this;
    }

    public ProductBuilder WithStock(int stock)
    {
        _stock = stock;
        return this;
    }

    public ProductBuilder WithCategoryId(CategoryId categoryId)
    {
        _categoryId = categoryId;
        return this;
    }

    public ProductBuilder WithMarkupPolicy(MarkupPolicy markupPolicy)
    {
        _markupPolicy = markupPolicy;
        return this;
    }

   
    public ProductBuilder WithSupplierId(SupplierId supplierId)
    {
        _supplierId = supplierId;
        return this;
    }

    public ProductBuilder WithBranchId(BranchId branchId)
    {
        _branchId = branchId;
        return this;
    }

    public Result<Product> Build()
    {
        return Product.Create(_name, _cost, _price, _stock, _categoryId, _markupPolicy, _supplierId, _branchId);
    }
}