using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class DolarConfigurations : IEntityTypeConfiguration<Dolar>
{
    public void Configure(EntityTypeBuilder<Dolar> builder)
    {
        builder.ToTable("DOLAR");
        
        builder
            .HasKey(c => c.Id)
            .HasName("PRIMARY");
        
        builder.HasIndex(a => a.Id, "DOLAR_ID_INDEX")
            .IsUnique();

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();

        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder
            .Property(c => c.Value)
            .IsRequired();
    }
}