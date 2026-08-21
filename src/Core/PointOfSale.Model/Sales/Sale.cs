using PointOfSale.Model.Customers;
using PointOfSale.Model.Products;
using PointOfSale.Shared;

namespace PointOfSale.Model.Sales;

public class Sale
{
    public SaleId Id { get; private set; }
    private readonly List<SaleDetail> _saleDetails;
    public IReadOnlyCollection<SaleDetail> SaleDetails => _saleDetails;
    public CustomerId CustomerId { get; private set; }
    public ProductId ProductId { get; private set; }

    protected Sale() { }

    private Sale(CustomerId customerId, ProductId productId)
    {
        Id = new SaleId(Guid.NewGuid());
        CustomerId = customerId;
        ProductId = productId;
        _saleDetails = new List<SaleDetail>();
    }

    public static Result<Sale> Create(CustomerId customerId, ProductId productId)
    {
        if (customerId == null || customerId.Value == Guid.Empty)
            return Result<Sale>.Failure("CustomerId cannot be empty.");

        if (productId == null || productId.Value == Guid.Empty)
            return Result<Sale>.Failure("ProductId cannot be empty.");

        var sale = new Sale(customerId, productId);
        return Result<Sale>.Success(sale);
    }

    public Result AddSaleDetail(int quantity, ProductId productId)
    {
        if (quantity <= 0)
            return Result.Failure("Quantity must be greater than zero.");

        if (productId == null || productId.Value == Guid.Empty)
            return Result.Failure("ProductId cannot be empty.");

        if (_saleDetails.Any(d => d.ProductId == productId))
            return Result.Failure("Product already exists in sale.");

        var saleDetail = SaleDetail.Create(quantity, productId, Id);
        _saleDetails.Add(saleDetail.Value);

        return Result.Success();
    }

    public Result RemoveSaleDetail(SaleDetail saleDetail)
    {
        if (!_saleDetails.Contains(saleDetail))
            return Result.Failure("Sale detail not found.");

        _saleDetails.Remove(saleDetail);
        return Result.Success();
    }
}