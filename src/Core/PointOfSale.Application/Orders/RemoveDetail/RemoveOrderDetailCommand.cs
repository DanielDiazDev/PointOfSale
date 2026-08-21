namespace PointOfSale.Application.Orders.RemoveOrderDetail;

public record RemoveOrderDetailCommand(Guid OrderId, Guid OrderDetailId);
