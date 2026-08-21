using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PointOfSale.Model.Categories;

namespace PointOfSale.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> e)
    {
        e.HasKey(x => x.Id);
        e.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new CategoryId(value));
        e.HasData(
                new
                {
                    Id = new CategoryId(Guid.Parse("5EEC68AA-E1F0-4699-B4A9-2E26C91B9E95")),
                    Name = "Beer"
                },
                new
                {
                    Id = new CategoryId(Guid.Parse("1B5DA60D-90A0-4A9A-B17C-30C799B0A9B3")),
                    Name = "Flower"
                });
        // e.HasData(
        //     Category.Seed(
        //         Guid.Parse("5EEC68AA-E1F0-4699-B4A9-2E26C91B9E95"),
        //         "Beer"),
        //     Category.Seed(
        //         Guid.Parse("1B5DA60D-90A0-4A9A-B17C-30C799B0A9B3"),
        //         "Flower")
        // );
    }
}