using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Configurations;

public class ProductConfiguration() : BaseConfiguration<Product>(TABLE_NAME) {
    private const string TABLE_NAME = "Products";

    public override void ConfigureAdditionalMappings(EntityTypeBuilder<Product> builder) {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2); 

        builder.Property(p => p.Description)
            .HasMaxLength(255);
    }
}