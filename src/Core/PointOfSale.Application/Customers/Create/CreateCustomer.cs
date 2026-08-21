using PointOfSale.Model.Customers;
using PointOfSale.Model.Repositories;
using PointOfSale.Shared;

namespace PointOfSale.Application.Customers.Create;

public class CreateCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Execute(CreateCustomerCommand command)
    {
        var customer = Customer.Create(command.Name, command.Phone, command.Address, command.Email, (Customer.IdTypes)command.IdType,
            command.NumberId);
        if (!customer.IsSuccess)
        {
            return Result<Guid>.Failure(customer.Error);
        }

        await _unitOfWork.Customers.AddAsync(customer.Value);

        return Result<Guid>.Success(customer.Value.Id.Value);
    }
}
