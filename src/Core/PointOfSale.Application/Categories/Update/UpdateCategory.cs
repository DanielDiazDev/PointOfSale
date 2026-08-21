using PointOfSale.Model.Categories;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Update;

public class UpdateCategory
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(UpdateCategoryCommand command)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(new CategoryId(command.Id));

        if (category is null)
            return Result.Failure("Category not found");

        category.Update(command.Name);

        await _unitOfWork.Categories.UpdateAsync(category);

        return Result.Success();
    }
}