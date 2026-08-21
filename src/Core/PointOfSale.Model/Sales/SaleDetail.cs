using PointOfSale.Model.Products;
using PointOfSale.Shared;

namespace PointOfSale.Model.Sales;

public class SaleDetail
{
    public SaleDetailId Id { get; private set; }
    public int Quantity { get; private set; }
    public ProductId ProductId { get; private set; }
    public SaleId SaleId { get; private set; }

    protected SaleDetail() { }

    private SaleDetail(int quantity, ProductId productId, SaleId saleId)
    {
        Id = new SaleDetailId(Guid.NewGuid());
        Quantity = quantity;
        ProductId = productId;
        SaleId = saleId;
    }

    public static Result<SaleDetail> Create(int quantity, ProductId productId, SaleId saleId)
    {
        if (quantity <= 0)
            return Result<SaleDetail>.Failure("Quantity must be greater than zero.");

        if (productId == null || productId.Value == Guid.Empty)
            return Result<SaleDetail>.Failure("ProductId cannot be empty.");

        if (saleId == null || saleId.Value == Guid.Empty)
            return Result<SaleDetail>.Failure("SaleId cannot be empty.");

        var detail = new SaleDetail(quantity, productId, saleId);
        return Result<SaleDetail>.Success(detail);
    }
}