using PointOfSale.Model.Customers;

namespace PointOfSale.Application.Customers.Update;

public record UpdateCustomerCommand(Guid Id, string Name, string Phone, string Address, string Email,
    int IdType, string NumberId);
