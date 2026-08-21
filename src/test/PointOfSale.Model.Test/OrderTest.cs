using NUnit.Framework;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Model.Test;

[TestFixture]
public class OrderTest
{
    [Test]
    public void Create_WhenSupplierIdIsValid_ShouldReturnSuccess()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var result = Order.Create(supplierId);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.SupplierId, Is.EqualTo(supplierId));
        Assert.That(result.Value.OrderDetails, Is.Empty);
    }

    [Test]
    public void Create_WhenSupplierIdIsEmpty_ShouldReturnFailure()
    {
        var result = Order.Create(new SupplierId(Guid.Empty));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("SupplierId cannot be empty."));
    }

    [Test]
    public void AddOrderDetail_WhenValidValues_ShouldReturnSuccessAndAddDetail()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var order = Order.Create(supplierId).Value;

        var productId = new ProductId(Guid.NewGuid());
        var result = order.AddOrderDetail(10, productId);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(order.OrderDetails, Has.Count.EqualTo(1));
        Assert.That(order.OrderDetails.First().NewStock, Is.EqualTo(10));
        Assert.That(order.OrderDetails.First().ProductId, Is.EqualTo(productId));
        Assert.That(order.OrderDetails.First().OrderId, Is.EqualTo(order.Id));
    }

    [Test]
    public void AddOrderDetail_WhenStockIsZero_ShouldReturnFailure()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var order = Order.Create(supplierId).Value;

        var productId = new ProductId(Guid.NewGuid());
        var result = order.AddOrderDetail(0, productId);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Stock must be greater than zero."));
    }

    [Test]
    public void AddOrderDetail_WhenProductIdIsEmpty_ShouldReturnFailure()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var order = Order.Create(supplierId).Value;

        var result = order.AddOrderDetail(5, new ProductId(Guid.Empty));

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("ProductId cannot be empty."));
    }

    [Test]
    public void RemoveOrderDetail_WhenDetailExists_ShouldReturnSuccessAndRemoveIt()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var order = Order.Create(supplierId).Value;

        var productId = new ProductId(Guid.NewGuid());
        order.AddOrderDetail(5, productId);
        var detail = order.OrderDetails.First();

        var result = order.RemoveOrderDetail(detail);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(order.OrderDetails, Is.Empty);
    }

    [Test]
    public void RemoveOrderDetail_WhenDetailDoesNotExist_ShouldReturnFailure()
    {
        var supplierId = new SupplierId(Guid.NewGuid());
        var order = Order.Create(supplierId).Value;

        var fakeDetail = OrderDetail.Create(5, new ProductId(Guid.NewGuid()), order.Id).Value;
        var result = order.RemoveOrderDetail(fakeDetail);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Order detail not found."));
    }
}
