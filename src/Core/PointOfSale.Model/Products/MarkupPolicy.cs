using PointOfSale.Model.Primitives;

namespace PointOfSale.Model.Products;

public class MarkupPolicy : ValueObject
{
    public bool CanMarkup {   get; private set; }
    public decimal MarkupPercentage {  get; private set; }

    protected MarkupPolicy(){}
    public MarkupPolicy(bool canMarkup, decimal markupPercentage)
    {
        if (canMarkup && markupPercentage <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(markupPercentage));
        }
        CanMarkup = canMarkup;
        MarkupPercentage = markupPercentage;
    }
   
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return CanMarkup;
        yield return MarkupPercentage;
    }
   

}