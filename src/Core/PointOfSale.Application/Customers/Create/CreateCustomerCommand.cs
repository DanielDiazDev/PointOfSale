using PointOfSale.Model.Customers;

namespace PointOfSale.Application.Customers.Create;

public record CreateCustomerCommand(string Name, string Phone, string Address, string Email, int IdType,
    string NumberId);
