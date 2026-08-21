using PointOfSale.Application.Sales.Get;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales;

public class GetSales
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSales(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetSaleResponse>>> Execute()
    {
        var sales = await _unitOfWork.Sales.GetAllAsync(s => s.SaleDetails);
        if (!sales.Any())
        {
            return Result<List<GetSaleResponse>>.Failure("No sales found");
        }

        var allDetails = sales.SelectMany(o => o.SaleDetails).ToList();

        var salesResponse = sales.Select(s =>
        {
            var details = allDetails
                .Where(sd => sd.SaleId == s.Id)
                .Select(sd => new GetSaleDetailResponse(sd.Id.Value, sd.Quantity, sd.ProductId.Value, sd.SaleId.Value))
                .ToList();

            return new GetSaleResponse(s.Id.Value, s.CustomerId.Value, s.ProductId.Value, details);
        }).ToList();

       

       return Result<List<GetSaleResponse>>.Success(salesResponse);
    }
}
