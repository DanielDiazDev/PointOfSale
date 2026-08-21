using PointOfSale.Model.Products;
using PointOfSale.Shared;

namespace PointOfSale.Model.Orders;

public class OrderDetail
{
    public OrderDetailId Id { get; private set; }
    public int NewStock { get; private set; }
    public ProductId ProductId { get; private set; }
    public DateTime DateCreated { get; private set; }
    public OrderId OrderId { get; private set; }

    protected OrderDetail() { }

    private OrderDetail(int newStock, ProductId productId, OrderId orderId)
    {
        Id = new OrderDetailId(Guid.NewGuid());
        NewStock = newStock;
        ProductId = productId;
        OrderId = orderId;
        DateCreated = DateTime.UtcNow;
    }

    public static Result<OrderDetail> Create(int newStock, ProductId productId, OrderId orderId)
    {
        if (newStock <= 0)
            return Result<OrderDetail>.Failure("Stock must be greater than zero.");

        if (productId == null || productId.Value == Guid.Empty)
            return Result<OrderDetail>.Failure("ProductId cannot be empty.");

        if (orderId == null || orderId.Value == Guid.Empty)
            return Result<OrderDetail>.Failure("OrderId cannot be empty.");

        var detail = new OrderDetail(newStock, productId, orderId);
        return Result<OrderDetail>.Success(detail);
    }
}