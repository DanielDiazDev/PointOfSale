namespace PointOfSale.Application.Sales.AddSaleDetail;

public record AddSaleDetailCommand(Guid SaleId, int Quantity, Guid ProductId);
