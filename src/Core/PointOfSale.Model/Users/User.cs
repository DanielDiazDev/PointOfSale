using PointOfSale.Shared;

namespace PointOfSale.Model.Users;

public class User
{
    public enum UserRole
    {
        Admin,
        Vendor,
        Supervisor
    }

    public UserId Id { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }

    private User(string name, string password, UserRole role)
    {
        Id = new UserId(Guid.NewGuid());
        Name = name;
        Password = password;
        Role = role;
    }

    public static Result<User> Create(string name, string password, UserRole role)
    {
        var validation = Validate(name, password);
        if (!validation.IsSuccess)
            return Result<User>.Failure(validation.Error);

        var user = new User(name, password, role);
        return Result<User>.Success(user);
    }

    public Result Update(string name, string password, UserRole role)
    {
        var validation = Validate(name, password);
        if (!validation.IsSuccess)
            return Result.Failure(validation.Error);

        Name = name;
        Password = password;
        Role = role;

        return Result.Success();
    }

    public Result<bool> VerifyPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result<bool>.Failure("Password cannot be empty.");

        return Result<bool>.Success(Password == password);
    }

    private static Result Validate(string name, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure("Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure("Password cannot be empty.");

        if (password.Length < 6)
            return Result.Failure("Password must be at least 6 characters long.");

        return Result.Success();
    }
}