using NUnit.Framework;
using PointOfSale.Model.Customers;
using PointOfSale.Shared;

namespace PointOfSale.Model.Test;

[TestFixture]
public class CustomerTest
{
    [Test]
    public void Create_WhenValidValues_ShouldReturnSuccessAndSetProperties()
    {
        var result = Customer.Create("John Doe", "123456789", "Main Street", "john@example.com", Customer.IdTypes.DNI, "987654");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Name, Is.EqualTo("John Doe"));
        Assert.That(result.Value.Phone, Is.EqualTo("123456789"));
        Assert.That(result.Value.Address, Is.EqualTo("Main Street"));
        Assert.That(result.Value.Email, Is.EqualTo("john@example.com"));
        Assert.That(result.Value.IdType, Is.EqualTo(Customer.IdTypes.DNI));
        Assert.That(result.Value.NumberId, Is.EqualTo("987654"));
    }

    [Test]
    public void Create_WhenNameIsEmpty_ShouldReturnFailure()
    {
        var result = Customer.Create("", "123456789", "Main Street", "john@example.com", Customer.IdTypes.DNI, "987654");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Customer name cannot be empty."));
    }

    [Test]
    public void Create_WhenPhoneIsEmpty_ShouldReturnFailure()
    {
        var result = Customer.Create("Jane Doe", "", "Main Street", "jane@example.com", Customer.IdTypes.DNI, "123456");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Phone cannot be empty."));
    }

    [Test]
    public void Create_WhenAddressIsEmpty_ShouldReturnFailure()
    {
        var result = Customer.Create("Jane Doe", "123456789", "", "jane@example.com", Customer.IdTypes.DNI, "123456");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Address cannot be empty."));
    }

    [Test]
    public void Create_WhenEmailIsEmpty_ShouldReturnFailure()
    {
        var result = Customer.Create("Jane Doe", "123456789", "Main Street", "", Customer.IdTypes.DNI, "123456");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Email cannot be empty."));
    }

    [Test]
    public void Create_WhenEmailFormatIsInvalid_ShouldReturnFailure()
    {
        var result = Customer.Create("Jane Doe", "123456789", "Main Street", "invalidEmail", Customer.IdTypes.DNI, "123456");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Email format is invalid."));
    }

    [Test]
    public void Create_WhenNumberIdIsEmpty_ShouldReturnFailure()
    {
        var result = Customer.Create("Jane Doe", "123456789", "Main Street", "jane@example.com", Customer.IdTypes.DNI, "");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("NumberId cannot be empty."));
    }

    [Test]
    public void Update_WhenValidValues_ShouldReturnSuccessAndUpdateProperties()
    {
        var customer = Customer.Create("John Doe", "123456789", "Main Street", "john@example.com", Customer.IdTypes.DNI, "987654").Value;

        var result = customer.Update("Johnny", "987654321", "Second Street", "johnny@example.com", Customer.IdTypes.DNI, "111222");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(customer.Name, Is.EqualTo("Johnny"));
        Assert.That(customer.Phone, Is.EqualTo("987654321"));
        Assert.That(customer.Address, Is.EqualTo("Second Street"));
        Assert.That(customer.Email, Is.EqualTo("johnny@example.com"));
        Assert.That(customer.NumberId, Is.EqualTo("111222"));
    }

    [Test]
    public void Update_WhenEmailFormatIsInvalid_ShouldReturnFailure()
    {
        var customer = Customer.Create("John Doe", "123456789", "Main Street", "john@example.com", Customer.IdTypes.DNI, "987654").Value;

        var result = customer.Update("John Doe", "123456789", "Main Street", "invalidEmail", Customer.IdTypes.DNI, "987654");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Email format is invalid."));
    }
}
