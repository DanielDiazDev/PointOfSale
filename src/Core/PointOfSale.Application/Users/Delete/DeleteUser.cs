using PointOfSale.Model.Repositories;
using PointOfSale.Model.Users;
using PointOfSale.Shared;

namespace PointOfSale.Application.Users.Delete;

public class DeleteUser
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUser(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(new UserId(id));

        if (user is null)
            return Result.Failure("User not found");

        await _unitOfWork.Users.DeleteAsync(user.Id);

        return Result.Success();
    }
}