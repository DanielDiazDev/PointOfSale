using PointOfSale.Application.Orders.Get;

namespace PointOfSale.Application.Orders;

public record GetOrderResponse(Guid Id, Guid SupplierId, List<GetOrderDetailResponse> OrderDetails);
