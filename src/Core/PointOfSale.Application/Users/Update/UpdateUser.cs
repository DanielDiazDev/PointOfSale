using PointOfSale.Model.Repositories;
using PointOfSale.Model.Services;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users.Update;

public class UpdateUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUser(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<Result> Execute(UpdateUserCommand command)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(new UserId(command.Id));

        if (user is null)
            return Result.Failure("User not found");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var result = user.Update(command.Name, hashedPassword, (User.UserRole)command.Role);
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Users.UpdateAsync(user);

        return Result.Success();
    }
}
