using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class CoinNetworkConfigurations : IEntityTypeConfiguration<CoinNetwork>
{
    public void Configure(EntityTypeBuilder<CoinNetwork> builder)
    {
        builder.ToTable("COINS_NETWORKS");

        builder.HasKey(cn => cn.Id)
            .HasName("PRIMARY");

        builder.HasKey(cn => new { cn.CoinId, cn.NetworkId })
            .HasName("FOREIGN");
        
        builder.HasIndex(a => a.Id, "COIN_NETWORK_ID_INDEX")
            .IsUnique();

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();

        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder.HasOne(cn => cn.Coin)
            .WithMany(c => c.CoinNetworks)
            .HasForeignKey(cn => cn.CoinId);

        builder.HasOne(cn => cn.Network)
            .WithMany(n => n.CoinNetworks)
            .HasForeignKey(cn => cn.NetworkId);
    }
}

