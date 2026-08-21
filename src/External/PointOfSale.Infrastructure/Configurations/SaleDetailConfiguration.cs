using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Products;
using PointOfSale.Model.Sales;

namespace PointOfSale.Infrastructure.Configurations;

public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
{
    public void Configure(EntityTypeBuilder<SaleDetail> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new SaleDetailId(value));
        e.Property(x => x.ProductId)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));
        e.HasOne<Product>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);
    }
}