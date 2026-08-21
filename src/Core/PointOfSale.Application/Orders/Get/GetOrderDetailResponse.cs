namespace PointOfSale.Application.Orders.Get;

public record GetOrderDetailResponse(Guid Id, int Stock, Guid ProductId, DateTime DateTime, Guid OrderId);