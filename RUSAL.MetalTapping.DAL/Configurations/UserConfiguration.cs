using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWID()")
            .IsRequired();

        builder.Property(b => b.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(256)")
            .IsRequired();

        builder.Property(b => b.Password)
            .HasColumnName("Password")
            .HasColumnType("nvarchar(256)")
            .IsRequired();

        builder.Property(b => b.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(b => b.LastName)
            .HasColumnName("LastName")
            .HasColumnType("nvarchar(100)")
            .IsRequired();
    }
}