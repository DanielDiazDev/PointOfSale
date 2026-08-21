namespace PointOfSale.Application.Orders.AddOrderDetail;

public record AddOrderDetailCommand(Guid OrderId, int NewStock, Guid ProductId);
