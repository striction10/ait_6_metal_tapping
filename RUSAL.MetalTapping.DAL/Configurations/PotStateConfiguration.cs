using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotStateConfiguration : IEntityTypeConfiguration<PotState>
{
    public void Configure(EntityTypeBuilder<PotState> builder)
    {
        builder.ToTable("PotState");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
    }
}