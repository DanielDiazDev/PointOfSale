using PointOfSale.Model.Customers;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Customers.Delete;

public class DeleteCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(new CustomerId(id));

        if (customer is null)
            return Result.Failure("Customer not found");

        await _unitOfWork.Customers.DeleteAsync(customer.Id);

        return Result.Success();
    }
}