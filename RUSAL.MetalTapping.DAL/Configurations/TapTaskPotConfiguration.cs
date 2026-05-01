using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class TapTaskPotConfiguration : IEntityTypeConfiguration<TapTaskPot>
{
    public void Configure(EntityTypeBuilder<TapTaskPot> builder)
    {
        builder.ToTable("TapTaskPot");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.PotMetalWeigth)
            .HasColumnName("PotMetalWeight")
            .HasColumnType("numeric(10, 0)")
            .IsRequired();

        builder.Property(b => b.MetalMarkAnalysisId)
            .HasColumnType("uniqueidentifier");
        
        builder.Property(b => b.TapTaskId)
            .HasColumnType("uniqueidentifier");
        
        builder.Property(b => b.PotId)
            .HasColumnType("uniqueidentifier");
        
        builder.HasOne(b => b.Analysis)
            .WithMany(b => b.TapTaskPots)
            .HasForeignKey(b => b.MetalMarkAnalysisId)
            .IsRequired();
        
        builder.HasOne(b => b.TapTask)
            .WithMany(b => b.TapTaskPots)
            .HasForeignKey(b => b.TapTaskId)
            .IsRequired();
        
        builder.HasOne(b => b.Pot)
            .WithMany(b => b.TapTaskPots)
            .HasForeignKey(b => b.PotId)
            .IsRequired();
    }
}