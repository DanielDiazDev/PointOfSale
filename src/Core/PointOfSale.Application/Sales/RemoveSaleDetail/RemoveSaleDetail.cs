using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.RemoveSaleDetail;

public class RemoveSaleDetail
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSaleDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid saleId, Guid saleDetailId)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(new SaleId(saleId), s => s.SaleDetails);

        if (sale is null)
            return Result.Failure("Sale not found");

        var detail = sale.SaleDetails.FirstOrDefault(sd => sd.Id == new SaleDetailId(saleDetailId));

        if (detail is null || detail.SaleId != sale.Id)
            return Result.Failure("Sale detail not found");

        sale.RemoveSaleDetail(detail);

        await _unitOfWork.Sales.UpdateAsync(sale);

        return Result.Success();
    }
}
