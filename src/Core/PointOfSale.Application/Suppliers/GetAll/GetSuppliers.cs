using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Suppliers;

public class GetSuppliers
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSuppliers(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetSupplierResponse>>> Execute()
    {
        var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
        if (!suppliers.Any())
        {
            return Result<List<GetSupplierResponse>>.Failure("No suppliers found");
        }

        var suppliersResponse = suppliers.Select(c => new GetSupplierResponse(c.Id.Value, c.Name, c.IdNumber, c.Phone,
            c.Address, c.Email)).ToList();

        return Result<List<GetSupplierResponse>>.Success(suppliersResponse);
    }
}
