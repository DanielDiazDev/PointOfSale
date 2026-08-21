using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Branches.Update;

public class UpdateBranch
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBranch(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(UpdateBranchCommand command)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(new BranchId(command.Id));

        if (branch is null)
            return Result.Failure("Branch not found");

        var result = branch.Update(command.Name);
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Branches.UpdateAsync(branch);

        return Result.Success();
    }
}
