namespace PointOfSale.Application.Suppliers.Create;

public record CreateSupplierCommand(string Name, int IdNumber, string Phone, string Address, string Email);
