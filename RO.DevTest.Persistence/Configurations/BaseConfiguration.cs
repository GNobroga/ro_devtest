using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Persistence.Configurations;

public abstract class BaseConfiguration<T>(string tableName) : IEntityTypeConfiguration<T> where T: BaseEntity {
    
    public void Configure(EntityTypeBuilder<T> builder) {
        builder.ToTable(tableName);
        builder.HasKey(p => p.Id);
        builder.Property(o => o.Id)
            .HasDefaultValueSql("uuid_generate_v4()");
        
        builder.Property(e => e.CreatedOn)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

         builder.Property(e => e.ModifiedOn)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
        
        ConfigureAdditionalMappings(builder);
    }

    public virtual void ConfigureAdditionalMappings(EntityTypeBuilder<T> builder) {}
}