using PointOfSale.Model.Primitives;

namespace PointOfSale.Model.Products;

public class Money : ValueObject
{
    protected Money(){}
    public Money(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Invalid money value");
        }
        Value = value;
    }

    public decimal Value { get; private set; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}