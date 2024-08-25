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

    public virtual DbSet<Crypto> Cryptos { get; set; }
    public virtual DbSet<CoinNetwork> CoinNetworks { get; set; }
    public virtual DbSet<Exchange> Exchanges { get; set; }
    public virtual DbSet<Network> Networks { get; set; }
    public virtual DbSet<OrderBook> OrderBooks { get; set; }
    public virtual DbSet<Opportunity> Opportunities { get; set; }
    public virtual DbSet<AdmConfiguration> AdmConfigurations { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Dolar> Dolar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseMySql("Server=localhost; Port=8889; Database=ARBITRAX; Uid=root; Connection Timeout=300; default command timeout=300; ConvertZeroDateTime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

