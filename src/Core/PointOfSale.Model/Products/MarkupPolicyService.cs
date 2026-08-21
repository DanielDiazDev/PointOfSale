namespace PointOfSale.Model.Products;

public class MarkupPolicyService
{
    public static decimal GetPriceFinal(decimal cost, decimal price, bool canMarkup, decimal markupPercentage)
    {
        if (!canMarkup)
        {
            return price;
        }
        else
        {
            return cost + (cost * (markupPercentage / 100));
        }
        // if (createProductCommand.CanMarkup && createProductCommand.MarkupPercentage > 0)
        // {
        //     var finalPriceValue = GetFinalPrice(createProductCommand);
    }
}