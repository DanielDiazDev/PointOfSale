using PointOfSale.Model.Primitives;

namespace PointOfSale.Model.Products;

public class StockIsNegativeAfterDecreaseException : DomainException
{
    public StockIsNegativeAfterDecreaseException(string message) : base(message)
    {
    }
}