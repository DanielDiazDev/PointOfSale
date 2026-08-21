namespace PointOfSale.Application.Sales.RemoveSaleDetail;

public record RemoveSaleDetailCommand(Guid SaleId, Guid SaleDetailId);
