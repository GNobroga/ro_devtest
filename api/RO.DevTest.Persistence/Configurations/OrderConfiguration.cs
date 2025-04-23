using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Configurations;

public class OrderConfiguration() : BaseConfiguration<Order>(TABLE_NAME) {

    private const string TABLE_NAME = "Orders";

    public override void ConfigureAdditionalMappings(EntityTypeBuilder<Order> builder) {
        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId);

        builder.Property(o => o.Status)
            .HasConversion<string>();

        builder.Ignore(o => o.Products);
    }

}