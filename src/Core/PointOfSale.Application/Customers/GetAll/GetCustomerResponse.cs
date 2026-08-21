using PointOfSale.Model.Customers;

namespace PointOfSale.Application.Customers;

public record GetCustomerResponse(Guid Id, string Name, string Phone, string Address, string Email,
    Customer.IdTypes IdType, string NumberId);
