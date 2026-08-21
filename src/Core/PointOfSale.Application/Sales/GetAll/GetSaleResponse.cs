using PointOfSale.Application.Sales.Get;

namespace PointOfSale.Application.Sales;

public record GetSaleResponse(Guid Id, Guid CustomerId, Guid ProductId, List<GetSaleDetailResponse> SaleDetails);
