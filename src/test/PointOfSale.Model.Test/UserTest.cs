using NUnit.Framework;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Model.Test;

[TestFixture]
public class UserTest
{
    [Test]
    public void Create_WhenValidValues_ShouldReturnSuccessAndSetProperties()
    {
        var result = User.Create("Alice", "password123", User.UserRole.Admin);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value.Name, Is.EqualTo("Alice"));
        Assert.That(result.Value.Password, Is.EqualTo("password123"));
        Assert.That(result.Value.Role, Is.EqualTo(User.UserRole.Admin));
    }

    [Test]
    public void Create_WhenNameIsEmpty_ShouldReturnFailure()
    {
        var result = User.Create("", "password123", User.UserRole.Vendor);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Name cannot be empty."));
    }

    [Test]
    public void Create_WhenPasswordIsEmpty_ShouldReturnFailure()
    {
        var result = User.Create("Bob", "", User.UserRole.Vendor);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Password cannot be empty."));
    }

    [Test]
    public void Create_WhenPasswordTooShort_ShouldReturnFailure()
    {
        var result = User.Create("Carl", "123", User.UserRole.Supervisor);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Password must be at least 6 characters long."));
    }

    [Test]
    public void Update_WhenValidValues_ShouldReturnSuccessAndUpdateProperties()
    {
        var user = User.Create("Charlie", "oldPass123", User.UserRole.Vendor).Value;

        var result = user.Update("Charles", "newPass123", User.UserRole.Supervisor);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(user.Name, Is.EqualTo("Charles"));
        Assert.That(user.Password, Is.EqualTo("newPass123"));
        Assert.That(user.Role, Is.EqualTo(User.UserRole.Supervisor));
    }

    [Test]
    public void Update_WhenNameIsEmpty_ShouldReturnFailure()
    {
        var user = User.Create("David", "pass123", User.UserRole.Admin).Value;

        var result = user.Update("", "newPass123", User.UserRole.Vendor);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Name cannot be empty."));
    }

    [Test]
    public void Update_WhenPasswordIsEmpty_ShouldReturnFailure()
    {
        var user = User.Create("Eve", "pass123", User.UserRole.Admin).Value;

        var result = user.Update("EveUpdated", "", User.UserRole.Supervisor);

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Password cannot be empty."));
    }

    [Test]
    public void VerifyPassword_WhenCorrectPassword_ShouldReturnTrue()
    {
        var user = User.Create("Frank", "securePass", User.UserRole.Vendor).Value;

        var result = user.VerifyPassword("securePass");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.True);
    }

    [Test]
    public void VerifyPassword_WhenIncorrectPassword_ShouldReturnFalse()
    {
        var user = User.Create("Grace", "securePass", User.UserRole.Vendor).Value;

        var result = user.VerifyPassword("wrongPass");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.False);
    }

    [Test]
    public void VerifyPassword_WhenEmptyPassword_ShouldReturnFailure()
    {
        var user = User.Create("Henry", "securePass", User.UserRole.Admin).Value;

        var result = user.VerifyPassword("");

        Assert.That(!result.IsSuccess, Is.True);
        Assert.That(result.Error, Is.EqualTo("Password cannot be empty."));
    }
}
