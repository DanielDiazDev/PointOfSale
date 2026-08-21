using PointOfSale.Model.Customers;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Customers.Update;

public class UpdateCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Execute(UpdateCustomerCommand command)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(new CustomerId(command.Id));

        if (customer is null)
            return Result.Failure("Customer not found");

        var result = customer.Update(command.Name, command.Phone, command.Address, command.Email, (Customer.IdTypes)command.IdType,
            command.NumberId);
        if (!result.IsSuccess)
            return result;

        await _unitOfWork.Customers.UpdateAsync(customer);

        return Result.Success();
    }
}
