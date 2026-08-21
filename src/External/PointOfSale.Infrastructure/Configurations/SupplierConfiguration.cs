using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Suppliers;

namespace PointOfSale.Infrastructure.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new SupplierId(value));
        e.HasData(
            new
            {
                Id = new SupplierId(Guid.Parse("673d7c4f-3ed3-43bf-99a7-13106ecb213d")),
                Name = "Supplier 1",
                IdNumber = 123,
                Phone = "123",
                Address = "123 Main St",
                Email = "123@email.com"
            });
       
    }
}