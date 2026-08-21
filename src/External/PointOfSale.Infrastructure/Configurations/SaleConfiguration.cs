using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Customers;
using PointOfSale.Model.Products;
using PointOfSale.Model.Sales;

namespace PointOfSale.Infrastructure.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new SaleId(value));
        e.Property(x => x.ProductId)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));
        e.HasOne<Product>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);
        e.Property(x => x.CustomerId)
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value));
        e.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(s => s.CustomerId);
        e.HasMany(o => o.SaleDetails)
            .WithOne()
            .HasForeignKey(od => od.SaleId);
    }
}