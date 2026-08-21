using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;
using PointOfSale.Shared;

namespace PointOfSale.Model.Suppliers;

public class Supplier
{
    public SupplierId Id { get; private set; }
    public string Name { get; private set; }
    public int IdNumber { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }

    protected Supplier() { }

    private Supplier(string name, int idNumber, string phone, string address, string email)
    {
        Id = new SupplierId(Guid.NewGuid());
        Name = name;
        IdNumber = idNumber;
        Phone = phone;
        Address = address;
        Email = email;
    }

    private Supplier(SupplierId id, string name, int idNumber, string phone, string address, string email)
    {
        Id = id;
        Name = name;
        IdNumber = idNumber;
        Phone = phone;
        Address = address;
        Email = email;
    }

    public static Result<Supplier> Create(string name, int idNumber, string phone, string address, string email)
    {
        var validation = Validate(name, idNumber, phone, address, email);
        if (!validation.IsSuccess)
            return Result<Supplier>.Failure(validation.Error);

        var supplier = new Supplier(name, idNumber, phone, address, email);
        return Result<Supplier>.Success(supplier);
    }

    public static Result<Supplier> Seed(Guid id, string name, int idNumber, string phone, string address, string email)
    {
        var validation = Validate(name, idNumber, phone, address, email);
        if (!validation.IsSuccess)
            return Result<Supplier>.Failure(validation.Error);

        var supplier = new Supplier(new SupplierId(id), name, idNumber, phone, address, email);
        return Result<Supplier>.Success(supplier);
    }

    public Result Update(string name, int idNumber, string phone, string address, string email)
    {
        var validation = Validate(name, idNumber, phone, address, email);
        if (!validation.IsSuccess)
            return Result.Failure(validation.Error);

        Name = name;
        IdNumber = idNumber;
        Phone = phone;
        Address = address;
        Email = email;

        return Result.Success();
    }

    private static Result Validate(string name, int idNumber, string phone, string address, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Supplier name cannot be empty.");

        if (idNumber <= 0)
            return Result.Failure("IdNumber must be greater than zero.");

        if (string.IsNullOrWhiteSpace(phone))
            return Result.Failure("Phone cannot be empty.");

        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure("Address cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure("Email cannot be empty.");

        if (!email.Contains('@'))
            return Result.Failure("Email format is invalid.");

        return Result.Success();
    }
}
