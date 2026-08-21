namespace PointOfSale.Application.Suppliers;

public record GetSupplierResponse(Guid Id, string Name, int IdNumber, string Phone, string Address, string Email);
