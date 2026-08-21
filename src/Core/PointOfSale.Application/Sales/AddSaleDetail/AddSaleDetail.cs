using PointOfSale.Model.Products;
using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.AddSaleDetail;

public class AddSaleDetail
{
    private readonly IUnitOfWork _unitOfWork;

    public AddSaleDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(AddSaleDetailCommand command)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(new SaleId(command.SaleId), s => s.SaleDetails);

        if (sale is null)
            return Result.Failure("Sale not found");

        var result = sale.AddSaleDetail(command.Quantity, new ProductId(command.ProductId));
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Sales.UpdateAsync(sale);

        return Result.Success();
    }
}
