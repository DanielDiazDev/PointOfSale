using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Products;

public class GetProducts
{
    private IUnitOfWork _unitOfWork;

    public GetProducts(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<GetProductResponse>>> Execute()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        if (!products.Any())
        {
            return Result<List<GetProductResponse>>.Failure("No products found");
        }

        var categoriesResponse = products.Select(c => new GetProductResponse(c.Id.Value, c.Name, c.Price.Value,
            c.Cost.Value
            , c.Stock, c.CategoryId.Value, c.MarkupPolicy.CanMarkup, c.MarkupPolicy.MarkupPercentage
            , c.SupplierId.Value, c.BranchId.Value)).ToList();
        return Result<List<GetProductResponse>>.Success(categoriesResponse);
      
    }
}