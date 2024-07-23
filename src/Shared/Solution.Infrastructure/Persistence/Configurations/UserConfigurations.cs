using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Core.Entities;

namespace Solution.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS");

        builder
            .HasKey(u => u.Id)
            .HasName("PRIMARY");
        
        builder.HasIndex(a => a.Id, "USER_ID_INDEX")
            .IsUnique();
        
        builder
            .Property(u => u.Name)
            .IsRequired();
        
        builder
            .Property(u => u.Email)
            .IsRequired();
        
        builder
            .Property(u => u.Password)
            .IsRequired();
        
        builder
            .Property(u => u.Level)
            .IsRequired();
        
        builder
            .Property(u => u.Trial)
            .IsRequired();
    }
}