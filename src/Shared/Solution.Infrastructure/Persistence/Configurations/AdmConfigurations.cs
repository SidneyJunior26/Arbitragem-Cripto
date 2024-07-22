using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class AdmConfigurations : IEntityTypeConfiguration<AdmConfiguration>
{
    public void Configure(EntityTypeBuilder<AdmConfiguration> builder)
    {
        builder.ToTable("ADM_CONFIGURATIONS");

        builder
            .HasKey(c => c.Id)
            .HasName("PRIMARY");

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();
        
        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder
            .Property(c => c.MinSpread)
            .IsRequired();
    }
}