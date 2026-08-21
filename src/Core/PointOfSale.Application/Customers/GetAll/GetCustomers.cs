using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Customers;

public class GetCustomers
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomers(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetCustomerResponse>>> Execute()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();
        if (!customers.Any())
        {
            return Result<List<GetCustomerResponse>>.Failure("No customers found");
        }

        var customersResponse = customers.Select(c => new GetCustomerResponse(c.Id.Value, c.Name, c.Phone, c.Address,
            c.Email, c.IdType, c.NumberId)).ToList();

        return Result<List<GetCustomerResponse>>.Success(customersResponse);
    }
}
