using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Suppliers.Delete;

public class DeleteSupplier
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSupplier(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(new SupplierId(id));

        if (supplier is null)
            return Result.Failure("Supplier not found");

        await _unitOfWork.Suppliers.DeleteAsync(supplier.Id);

        return Result.Success();
    }
}