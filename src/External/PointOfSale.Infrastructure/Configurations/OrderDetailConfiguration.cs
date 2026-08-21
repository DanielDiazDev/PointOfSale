using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Orders;
using PointOfSale.Model.Products;

namespace PointOfSale.Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> e)
    {
        e.HasKey(od => od.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new OrderDetailId(value));
        e.Property(x => x.ProductId)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));

        e.HasOne<Product>()
            .WithMany()
            .HasForeignKey(od => od.ProductId);
    }
}