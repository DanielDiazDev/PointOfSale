using PointOfSale.Model.Suppliers;

namespace PointOfSale.Application.Suppliers.Update;

public record UpdateSupplierCommand(Guid Id, string Name, int IdNumber, string Phone, string Address, string Email);
