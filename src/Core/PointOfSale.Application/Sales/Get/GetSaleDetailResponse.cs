namespace PointOfSale.Application.Sales.Get;

public record GetSaleDetailResponse(Guid Id, int Quantity, Guid ProductId, Guid SaleId);