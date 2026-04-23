using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotConfiguration : IEntityTypeConfiguration<PotModel>
{
    public void Configure(EntityTypeBuilder<PotModel> builder)
    {
        builder.ToTable("Pot");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(b => b.StateId)
            .HasColumnType("uniqueidentifier");
        
        builder.Property(b => b.BuildingId)
            .HasColumnType("uniqueidentifier");
        
        builder.HasOne(b => b.Building)
            .WithMany(b => b.Pots)
            .HasForeignKey(b => b.BuildingId)
            .IsRequired();

        builder.HasOne(b => b.State)
            .WithMany(b => b.Pots)
            .HasForeignKey(b => b.StateId)
            .IsRequired();
    }
}
