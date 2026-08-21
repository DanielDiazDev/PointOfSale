namespace PointOfSale.Model.Products;

public record struct ProductId(Guid Value);
// {
//     // public Guid Value { get; private set; }
//     //
//     // public ProductId(Guid value)
//     // {
//     //     if (value == Guid.Empty)
//     //     {
//     //         throw new ArgumentException("Value cannot be empty or null");
//     //     }
//     //     Value = value;
//     // }
// }