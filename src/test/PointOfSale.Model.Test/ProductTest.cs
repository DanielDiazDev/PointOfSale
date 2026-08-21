using NUnit.Framework;
using PointOfSale.Model.Categories;
using PointOfSale.Model.Primitives;
using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Model.Test;

[TestFixture]
public class ProductTest
{
    [Test]
    public void Create_WhenNameIsEmpty_ShouldReturnFailure()
    {
        var result = Product.Create("", new Money(10), new Money(5), 5,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid()));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Invalid product name or image url."));
    }

    [Test]
    public void Create_WhenStockIsZero_ShouldReturnFailure()
    {
        var result = Product.Create("Test", new Money(10), new Money(5), 0,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid()));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Stock cannot be negative or zero."));
    }

    [Test]
    public void Create_WhenValidValues_ShouldReturnSuccess()
    {
        var result = Product.Create("Test", new Money(10), new Money(5), 10,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10),new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid()));

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Name, Is.EqualTo("Test"));
        Assert.That(result.Value.Stock, Is.EqualTo(10));
    }

    [Test]
    public void Update_WhenNameIsEmpty_ShouldReturnFailure()
    {
        var product = Product.Create("Test", new Money(10), new Money(5), 10,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10),
            new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid())).Value;

        var result = product.Update("", new Money(20), new Money(10), 15,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 5), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid()));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Name cannot be empty."));
    }

    [Test]
    public void AddStock_WhenValid_ShouldIncreaseStock()
    {
        var product = Product.Create("Test", new Money(10), new Money(5), 5,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid())).Value;

        var result = product.AddStock(3);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(product.Stock, Is.EqualTo(8));
    }

    [Test]
    public void AddStock_WhenZero_ShouldReturnFailure()
    {
        var product = Product.Create("Test", new Money(10), new Money(5), 5,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid())).Value;

        var result = product.AddStock(0);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Stock to add must be greater than zero."));
    }

    [Test]
    public void DecreaseStock_WhenValid_ShouldDecreaseStock()
    {
        var product = Product.Create("Test", new Money(10), new Money(5), 5,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid())).Value;

        var result = product.DecreaseStock(2);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(product.Stock, Is.EqualTo(3));
    }

    [Test]
    public void DecreaseStock_WhenGreaterThanCurrent_ShouldReturnFailure()
    {
        var product = Product.Create("Test", new Money(10), new Money(5), 3,
            new CategoryId(Guid.NewGuid()), new MarkupPolicy(true, 10), new SupplierId(Guid.NewGuid()), new BranchId(Guid.NewGuid())).Value;

        var result = product.DecreaseStock(5);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Stock cannot be negative after decrease."));
    }
}
