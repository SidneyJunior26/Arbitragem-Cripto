using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class OpportunityConfigurations : IEntityTypeConfiguration<Opportunity>
{
    public void Configure(EntityTypeBuilder<Opportunity> builder)
    {
        builder.ToTable("OPPORTUNITIES");

        builder
            .HasKey(a => a.Id)
            .HasName("PRIMARY");
        
        builder.HasIndex(a => a.Id, "OPPORTUNITY_ID_INDEX")
            .IsUnique();

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();

        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder
            .Property(a => a.LowerValue)
            .IsRequired();

        builder
            .Property(a => a.HighestValue)
            .IsRequired();

        builder
            .Property(a => a.DifferencePercentage)
            .IsRequired();

        builder
            .Property(a => a.CoinId)
            .IsRequired();

        builder
            .HasOne(a => a.Coin)
            .WithMany(c => c.Opportunities)
            .HasForeignKey(a => a.CoinId);

        builder
            .Property(a => a.ExchangeToBuyId)
            .IsRequired();

        builder
            .HasOne(a => a.ExchangeToBuy)
            .WithMany(e => e.Opportunities)
            .HasForeignKey(a => a.ExchangeToBuy);

        builder
            .Property(a => a.ExchangeToSellId)
            .IsRequired();

        builder
            .HasOne(a => a.ExchangeToSell)
            .WithMany(e => e.Opportunities)
            .HasForeignKey(a => a.ExchangeToBuyId);
    }
}

