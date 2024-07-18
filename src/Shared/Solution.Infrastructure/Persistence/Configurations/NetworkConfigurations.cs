using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class NetworkConfigurations : IEntityTypeConfiguration<Network>
{
    public void Configure(EntityTypeBuilder<Network> builder)
    {
        builder.ToTable("NETWORKS");

        builder
            .HasKey(n => n.Id)
            .HasName("PRIMARY");

        builder
            .Property(n => n.Name)
            .IsRequired();

        builder
            .HasMany(n => n.CoinNetworks)
            .WithOne(cn => cn.Network);
    }
}

