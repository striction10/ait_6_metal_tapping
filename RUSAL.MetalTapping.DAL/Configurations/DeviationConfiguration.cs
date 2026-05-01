using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class DeviationConfiguration : IEntityTypeConfiguration<Deviation>
{
    public void Configure(EntityTypeBuilder<Deviation> builder)
    {
        builder.ToTable("Deviation");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.ActualMetalLevel)
            .HasColumnName("ActualMetalLevel")
            .HasColumnType("numeric(4, 0)");

        builder.Property(b => b.TargetMetalLevel)
            .HasColumnName("TargetMetalLevel")
            .HasColumnType("numeric(4, 0)")
            .IsRequired();

        builder.Property(b => b.IsValid)
            .HasColumnName("IsValid")
            .HasColumnType("bit");
           
        builder.HasOne(b => b.PotReglament)
            .WithMany(b => b.Deviations)
            .HasForeignKey(b => b.PotReglamentId)
            .IsRequired();

        builder.Property(b => b.PotReglamentId)
            .HasColumnType("uniqueidentifier");
    }
}