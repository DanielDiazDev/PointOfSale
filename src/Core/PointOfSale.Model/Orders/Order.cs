using PointOfSale.Model.Products;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Model.Orders;

public class Order
{
    public OrderId Id { get; private set; }
    public SupplierId SupplierId { get; private set; }
    private readonly List<OrderDetail> _orderDetails;
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;

    protected Order() { }

    private Order(SupplierId supplierId)
    {
        Id = new OrderId(Guid.NewGuid());
        _orderDetails = new List<OrderDetail>();
        SupplierId = supplierId;
    }

    public static Result<Order> Create(SupplierId supplierId)
    {
        if (supplierId == null || supplierId.Value == Guid.Empty)
            return Result<Order>.Failure("SupplierId cannot be empty.");


        var order = new Order(supplierId);
        return Result<Order>.Success(order);
    }

    public Result AddOrderDetail(int newStock, ProductId productId)
    {
        if (newStock <= 0)
            return Result.Failure("Stock must be greater than zero.");

        if (productId == null || productId.Value == Guid.Empty)
            return Result.Failure("ProductId cannot be empty.");

        if (_orderDetails.Any(d => d.ProductId == productId))
        {
            return Result.Failure("Product already exists in order.");
            
        }

        var orderDetail = OrderDetail.Create(newStock, productId, Id);
        if (!orderDetail.IsSuccess)
        {
            return orderDetail;
        }
        _orderDetails.Add(orderDetail.Value);

        return Result.Success();
    }

    public Result RemoveOrderDetail(OrderDetail orderDetail)
    {
        if (!_orderDetails.Contains(orderDetail))
            return Result.Failure("Order detail not found.");

        _orderDetails.Remove(orderDetail);
        return Result.Success();
    }
}
