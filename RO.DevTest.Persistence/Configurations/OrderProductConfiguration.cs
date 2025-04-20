using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Configurations;

public class OrderProductConfiguration() : BaseConfiguration<OrderProduct>(TABLE_NAME) {

    private const string TABLE_NAME = "OrderProducts";

    public override void ConfigureAdditionalMappings(EntityTypeBuilder<OrderProduct> builder) {
        builder.HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        builder.HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(op => op.ProductId);

        builder.Property(op => op.Quantity)
            .HasDefaultValue(1);
    }
}