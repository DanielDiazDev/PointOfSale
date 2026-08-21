using PointOfSale.Model.Repositories;
using PointOfSale.Model.Sales;
using PointOfSale.Shared;

namespace PointOfSale.Application.Sales.Delete;

public class DeleteSale
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSale(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(new SaleId(id));

        if (sale is null)
            return Result.Failure("Sale not found");

        await _unitOfWork.Sales.DeleteAsync(sale.Id);

        return Result.Success();
    }
}