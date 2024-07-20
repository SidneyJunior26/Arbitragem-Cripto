using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class OrderBookConfigurations : IEntityTypeConfiguration<OrderBook>
{
    public void Configure(EntityTypeBuilder<OrderBook> builder)
    {
        builder.ToTable("ORDER_BOOKS");

        builder
            .HasKey(ob => ob.Id)
            .HasName("PRIMARY");
        
        builder
            .Property(ob => ob.Order)
            .IsRequired();

        builder
            .Property(ob => ob.Side)
            .IsRequired();

        builder
            .Property(ob => ob.Value)
            .IsRequired();

        builder
            .Property(ob => ob.Amount)
            .IsRequired();

        builder
            .Property(ob => ob.TotalValue)
            .IsRequired();

        builder
            .Property(ob => ob.ExchangeId)
            .IsRequired();

        builder.HasOne(ob => ob.Exchange)
            .WithMany(e => e.OrderBooks)
            .HasForeignKey(ob => ob.ExchangeId);

        builder
            .Property(ob => ob.CoinId)
            .IsRequired();

        builder
            .HasOne(ob => ob.Coin)
            .WithMany(c => c.OrderBooks)
            .HasForeignKey(ob => ob.CoinId);
    }
}

