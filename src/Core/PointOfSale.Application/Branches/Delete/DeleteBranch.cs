using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Branches.Delete;

public class DeleteBranch
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBranch(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(new BranchId(id));

        if (branch is null)
            return Result.Failure("Branch not found");

        await _unitOfWork.Branches.DeleteAsync(branch.Id);

        return Result.Success();
    }
}