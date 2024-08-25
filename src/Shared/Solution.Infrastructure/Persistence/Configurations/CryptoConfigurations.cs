using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class CryptoConfigurations : IEntityTypeConfiguration<Crypto>
{
    public void Configure(EntityTypeBuilder<Crypto> builder)
    {
        builder.ToTable("CRYPTOS");

        builder
            .HasKey(c => c.Id)
            .HasName("PRIMARY");
        
        builder.HasIndex(a => a.Id, "COIN_ID_INDEX")
            .IsUnique();

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();

        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder
            .Property(c => c.Symbol)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .IsRequired();
        
        builder
            .Property(c => c.Active)
            .HasDefaultValue(true);

        builder.HasMany(c => c.OrderBooks)
            .WithOne(ob => ob.Crypto)
            .HasForeignKey(c => c.CoinId);

        builder
            .HasMany(c => c.CoinNetworks)
            .WithOne(cn => cn.Crypto)
            .HasForeignKey(cn => cn.CoinId);

        builder
            .HasMany(c => c.Opportunities)
            .WithOne(o => o.Crypto)
            .HasForeignKey(o => o.CoinId);
    }
}

