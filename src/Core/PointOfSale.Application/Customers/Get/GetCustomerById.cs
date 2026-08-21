using PointOfSale.Model.Customers;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Customers.Get;

public class GetCustomerById
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCustomerById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<GetCustomerResponse>> Execute(GetCustomerByIdQuery query)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(new CustomerId(query.Id));

        if (customer is null)
            return Result<GetCustomerResponse>.Failure("Customer not found");

        var response = new GetCustomerResponse(customer.Id.Value, customer.Name, customer.Phone, customer.Address,
            customer.Email, customer.IdType, customer.NumberId);

        return Result<GetCustomerResponse>.Success(response);
    }
}
