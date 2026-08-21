using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.Get;

public class GetSaleById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetSaleResponse>> Execute(GetSaleByIdQuery query)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(new SaleId(query.Id), s => s.SaleDetails);

        if (sale is null)
            return Result<GetSaleResponse>.Failure("Sale not found");
        
        var details = sale.SaleDetails
            .Select(d => new GetSaleDetailResponse(
                d.Id.Value, 
                d.Quantity, 
                d.ProductId.Value, 
                d.SaleId.Value
            ))
            .ToList();

        var response = new GetSaleResponse(sale.Id.Value, sale.CustomerId.Value, sale.ProductId.Value, details);

        return Result<GetSaleResponse>.Success(response);
    }
}