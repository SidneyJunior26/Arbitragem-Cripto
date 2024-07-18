using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class ExchangeConfigurations : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.ToTable("EXCHANGES");

        builder
            .HasKey(e => e.Id)
            .HasName("PRIMARY");

        builder
            .Property(c => c.RegisterDate)
            .IsRequired();

        builder
            .Property(c => c.UpdateDate)
            .IsRequired();

        builder
            .Property(e => e.Name)
            .IsRequired();

        builder
            .HasMany(e => e.Opportunities)
            .WithOne(ob => ob.ExchangeToBuy)
            .HasForeignKey(ob => ob.ExchangeToBuyId);

        builder
            .HasMany(e => e.Opportunities)
            .WithOne(ob => ob.ExchangeToSell)
            .HasForeignKey(ob => ob.ExchangeToSellId);
    }
}

