using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coin> Coins { get; set; }
    public virtual DbSet<CoinNetwork> CoinNetworks { get; set; }
    public virtual DbSet<Exchange> Exchanges { get; set; }
    public virtual DbSet<Network> Networks { get; set; }
    public virtual DbSet<OrderBook> OrderBooks { get; set; }
    public virtual DbSet<Opportunity> Opportunities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

