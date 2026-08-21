using PointOfSale.Model.Primitives;

namespace PointOfSale.Model.Products;

public class StockIsNegativeOrZeroException : DomainException
{
    public StockIsNegativeOrZeroException(string message) : base(message)
    {
    }
}