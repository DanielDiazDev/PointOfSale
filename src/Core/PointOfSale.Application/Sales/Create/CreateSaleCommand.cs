namespace PointOfSale.Application.Sales.Create;

public record CreateSaleCommand(Guid CustomerId, Guid ProductId);
