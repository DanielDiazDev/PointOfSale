using PointOfSale.Model.Categories;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Delete;

public class DeleteCategory
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(new CategoryId(id));

        if (category is null)
            return Result.Failure("Category not found");

        await _unitOfWork.Categories.DeleteAsync(category.Id);

        return Result.Success();
    }
}