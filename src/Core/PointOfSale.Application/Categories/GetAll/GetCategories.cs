using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories;

public class GetCategories
{
    private readonly IUnitOfWork  _unitOfWork;

    public GetCategories(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetCategoryResponse>>> Execute()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        if (!categories.Any())
        {
            return Result<List<GetCategoryResponse>>.Failure("No Categories found");
        }
        var categoriesResponse = categories.Select(c => new GetCategoryResponse(c.Id.Value, c.Name)).ToList();
        
        return Result<List<GetCategoryResponse>>.Success(categoriesResponse);
    }
}