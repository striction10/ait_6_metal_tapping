using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class ScoopUsageConfiguration : IEntityTypeConfiguration<ScoopUsage>
{
    public void Configure(EntityTypeBuilder<ScoopUsage> builder)
    {
        builder.ToTable("ScoopUsage");

        builder.HasKey(su => su.Id);

        builder.Property(su => su.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(su => su.ScoopId)
            .HasColumnName("ScoopId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(su => su.BusyFrom)
            .HasColumnName("BusyFrom")
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(su => su.BusyUntil)
            .HasColumnName("BusyUntil")
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.HasOne(su => su.Scoop)
            .WithMany(su => su.ScoopUsageModels)
            .HasForeignKey(su => su.ScoopId);
    }
}