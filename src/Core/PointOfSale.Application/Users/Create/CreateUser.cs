using PointOfSale.Model.Repositories;
using PointOfSale.Model.Services;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users.Create;

public class CreateUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUser(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<Result<Guid>> Execute(CreateUserCommand command)
    {
        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = User.Create(command.Name, hashedPassword, (User.UserRole)command.Role);
        if (!user.IsSuccess)
        {
            return Result<Guid>.Failure(user.Error);
        }

        await _unitOfWork.Users.AddAsync(user.Value);

        return Result<Guid>.Success(user.Value.Id.Value);
    }
}
