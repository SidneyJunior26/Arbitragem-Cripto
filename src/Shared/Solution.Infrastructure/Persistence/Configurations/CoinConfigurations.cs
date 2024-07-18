using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class CoinConfigurations : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.ToTable("COINS");

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
            .Property(c => c.Symbol)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .IsRequired();

        builder.HasMany(c => c.OrderBooks)
            .WithOne(ob => ob.Coin)
            .HasForeignKey(c => c.CoinId);

        builder
            .HasMany(c => c.CoinNetworks)
            .WithOne(cn => cn.Coin)
            .HasForeignKey(cn => cn.CoinId);

        builder
            .HasMany(c => c.Opportunities)
            .WithOne(o => o.Coin)
            .HasForeignKey(o => o.CoinId);
    }
}

