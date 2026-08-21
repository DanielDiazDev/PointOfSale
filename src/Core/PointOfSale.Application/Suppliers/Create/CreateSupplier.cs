using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Suppliers.Create;

public class CreateSupplier
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSupplier(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Execute(CreateSupplierCommand command)
    {
        var supplier = Supplier.Create(command.Name, command.IdNumber, command.Phone, command.Address, command.Email);
        if (!supplier.IsSuccess)
        {
            return Result<Guid>.Failure(supplier.Error);
        }

        await _unitOfWork.Suppliers.AddAsync(supplier.Value);

        return Result<Guid>.Success(supplier.Value.Id.Value);
    }
}
