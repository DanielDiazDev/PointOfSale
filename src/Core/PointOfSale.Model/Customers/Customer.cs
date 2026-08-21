using PointOfSale.Model.Primitives;
using PointOfSale.Shared;

namespace PointOfSale.Model.Customers;

public class Customer
{
    public CustomerId Id { get; private set; }
    public string Name { get; private set; }
    public enum IdTypes
    {
        DNI
    }
    public IdTypes IdType { get; private set; }
    public string NumberId { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }

    protected Customer() { }

    private Customer(string name, string phone, string address, string email, IdTypes idType, string numberId)
    {
        Id = new CustomerId(Guid.NewGuid());
        Name = name;
        Phone = phone;
        Address = address;
        Email = email;
        IdType = idType;
        NumberId = numberId;
    }

    public static Result<Customer> Create(string name, string phone, string address, string email, IdTypes idType, string numberId)
    {
        var validation = Validate(name, phone, address, email, numberId);
        if (!validation.IsSuccess)
        {
            return Result<Customer>.Failure(validation.Error);
        }

        var customer = new Customer(name, phone, address, email, idType, numberId);
        return Result<Customer>.Success(customer);
    }

    public Result Update(string name, string phone, string address, string email, IdTypes idType, string numberId)
    {
        var validation = Validate(name, phone, address, email, numberId);
        if (!validation.IsSuccess)
        {
            return Result.Failure(validation.Error);
        }

        Name = name;
        Phone = phone;
        Address = address;
        Email = email;
        IdType = idType;
        NumberId = numberId;

        return Result.Success();
    }

    private static Result Validate(string name, string phone, string address, string email, string numberId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure("Customer name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(phone))
        {
            return Result.Failure("Phone cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            return Result.Failure("Address cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure("Email cannot be empty.");
        }

        if (!email.Contains("@"))
        {
            return Result.Failure("Email format is invalid.");
        }

        if (string.IsNullOrWhiteSpace(numberId))
        {
            return Result.Failure("NumberId cannot be empty.");
        }

        return Result.Success();
    }
}
