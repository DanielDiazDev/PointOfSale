using NUnit.Framework;
using PointOfSale.Model.Customers;
using PointOfSale.Model.Products;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Model.Test;

[TestFixture]
public class SaleTest
{
    [Test]
    public void Create_WhenCustomerAndProductAreValid_ShouldReturnSuccess()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var productId = new ProductId(Guid.NewGuid());

        var result = Sale.Create(customer.Value.Id, productId);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.CustomerId, Is.EqualTo(customer.Value.Id));
        Assert.That(result.Value.ProductId, Is.EqualTo(productId));
        Assert.That(result.Value.SaleDetails, Is.Empty);
    }

    [Test]
    public void Create_WhenCustomerIdIsEmpty_ShouldReturnFailure()
    {
        var result = Sale.Create(new CustomerId(Guid.Empty), new ProductId(Guid.NewGuid()));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("CustomerId cannot be empty."));
    }

    [Test]
    public void Create_WhenProductIdIsEmpty_ShouldReturnFailure()
    {
        var customer = Customer.Create("Jane Doe", "99999", "Address", "email@", Customer.IdTypes.DNI, "5678");
        var result = Sale.Create(customer.Value.Id, new ProductId(Guid.Empty));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("ProductId cannot be empty."));
    }

    [Test]
    public void AddSaleDetail_WhenValidValues_ShouldReturnSuccessAndAddDetail()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var sale = Sale.Create(customer.Value.Id, new ProductId(Guid.NewGuid())).Value;

        var productId = new ProductId(Guid.NewGuid());
        var result = sale.AddSaleDetail(3, productId);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(sale.SaleDetails.Count, Is.EqualTo(1));
        Assert.That(sale.SaleDetails.First().Quantity, Is.EqualTo(3));
        Assert.That(sale.SaleDetails.First().ProductId, Is.EqualTo(productId));
        Assert.That(sale.SaleDetails.First().SaleId, Is.EqualTo(sale.Id));
    }

    [Test]
    public void AddSaleDetail_WhenQuantityIsZero_ShouldReturnFailure()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var sale = Sale.Create(customer.Value.Id, new ProductId(Guid.NewGuid())).Value;

        var result = sale.AddSaleDetail(0, new ProductId(Guid.NewGuid()));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Quantity must be greater than zero."));
    }

    [Test]
    public void AddSaleDetail_WhenProductAlreadyExists_ShouldReturnFailure()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var sale = Sale.Create(customer.Value.Id, new ProductId(Guid.NewGuid())).Value;

        var productId = new ProductId(Guid.NewGuid());
        sale.AddSaleDetail(2, productId);

        var result = sale.AddSaleDetail(5, productId);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Product already exists in sale."));
    }

    [Test]
    public void RemoveSaleDetail_WhenDetailExists_ShouldReturnSuccessAndRemoveIt()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var sale = Sale.Create(customer.Value.Id, new ProductId(Guid.NewGuid())).Value;

        var productId = new ProductId(Guid.NewGuid());
        sale.AddSaleDetail(2, productId);
        var detail = sale.SaleDetails.First();

        var result = sale.RemoveSaleDetail(detail);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(sale.SaleDetails, Is.Empty);
    }

    [Test]
    public void RemoveSaleDetail_WhenDetailDoesNotExist_ShouldReturnFailure()
    {
        var customer = Customer.Create("John Doe", "121313", "Address", "email@", Customer.IdTypes.DNI, "1234");
        var sale = Sale.Create(customer.Value.Id, new ProductId(Guid.NewGuid())).Value;

        var fakeDetail = SaleDetail.Create(2, new ProductId(Guid.NewGuid()), sale.Id).Value;
        var result = sale.RemoveSaleDetail(fakeDetail);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Sale detail not found."));
    }
}
