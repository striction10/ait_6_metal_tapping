using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();
        
        builder.Property(b => b.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(b => b.Password)
            .HasColumnName("Password")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(b => b.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(b => b.LastName)
            .HasColumnName("LastName")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
    }
}