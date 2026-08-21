using PointOfSale.Model.Repositories;
using PointOfSale.Model.Suppliers;
using PointOfSale.Shared;

namespace PointOfSale.Application.Suppliers.Get;

public class GetSupplierById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSupplierById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetSupplierResponse>> Execute(GetSupplierByIdQuery query)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(new SupplierId(query.Id));

        if (supplier is null)
            return Result<GetSupplierResponse>.Failure("Supplier not found");

        var response = new GetSupplierResponse(supplier.Id.Value, supplier.Name, supplier.IdNumber, supplier.Phone,
            supplier.Address, supplier.Email);

        return Result<GetSupplierResponse>.Success(response);
    }
}
