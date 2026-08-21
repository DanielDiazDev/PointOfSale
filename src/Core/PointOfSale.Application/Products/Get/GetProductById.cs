using PointOfSale.Application.Products;
using PointOfSale.Model.Categories;
using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Categories.Get;

public class GetProductById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetProductResponse>> Execute(GetProductByIdQuery query)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(new ProductId(query.Id));

        if (product is null)
            return Result<GetProductResponse>.Failure("Product not found");

        var response = new GetProductResponse(product.Id.Value, product.Name, product.Price.Value, product.Cost.Value, product.Stock,  product.CategoryId.Value, product.MarkupPolicy.CanMarkup, 
            product.MarkupPolicy.MarkupPercentage,  product.SupplierId.Value, product.BranchId.Value);

        return Result<GetProductResponse>.Success(response);
    }
}