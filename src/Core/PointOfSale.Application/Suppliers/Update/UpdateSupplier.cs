using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Suppliers.Update;

public class UpdateSupplier
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSupplier(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(UpdateSupplierCommand command)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(new SupplierId(command.Id));

        if (supplier is null)
            return Result.Failure("Supplier not found");

        var result = supplier.Update(command.Name, command.IdNumber, command.Phone, command.Address, command.Email);
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Suppliers.UpdateAsync(supplier);

        return Result.Success();
    }
}
