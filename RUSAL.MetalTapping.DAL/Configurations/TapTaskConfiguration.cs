using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class TapTaskConfiguration : IEntityTypeConfiguration<TapTask>
{
    public void Configure(EntityTypeBuilder<TapTask> builder)
    {
        builder.ToTable("TapTask");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();

        builder.Property(b => b.BuildingId)
            .HasColumnType("numeric(37, 0)");
        
        builder.Property(b => b.ScoopId)
            .HasColumnType("numeric(37, 0)");
        
        builder.Property(b => b.OrderId)
            .HasColumnType("numeric(37, 0)");
        
        builder.HasOne(b => b.Scoop)
            .WithMany(b => b.TapTasks)
            .HasForeignKey(b => b.ScoopId)
            .IsRequired();
        
        builder.HasOne(b => b.Building)
            .WithMany(b => b.TapTasks)
            .HasForeignKey(b => b.BuildingId)
            .IsRequired();
            
        builder.HasOne(b => b.Order)
            .WithMany(b => b.TapTasks)
            .HasForeignKey(b => b.OrderId)
            .IsRequired();
    }
}