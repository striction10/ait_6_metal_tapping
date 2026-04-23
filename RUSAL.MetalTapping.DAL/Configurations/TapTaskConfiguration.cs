using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class TapTaskConfiguration : IEntityTypeConfiguration<TapTaskModel>
{
    public void Configure(EntityTypeBuilder<TapTaskModel> builder)
    {
        builder.ToTable("TapTasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(t => t.Building)
            .WithMany(b => b.TapTasks)
            .HasForeignKey(t => t.BuildingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Order)
            .WithMany(o => o.TapTasks)
            .HasForeignKey(t => t.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Scoop)
            .WithMany(s => s.TapTasks)
            .HasForeignKey(t => t.ScoopId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}