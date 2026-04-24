using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class DeviationValuesConfiguration : IEntityTypeConfiguration<DeviationValuesModel>
{
    public void Configure(EntityTypeBuilder<DeviationValuesModel> builder)
    {
        builder.ToTable("DeviationValues");

        builder.Property(b => b.Value)
            .HasColumnName("Value")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(b => b.CastingRatio)
            .HasColumnName("CastingRatio")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(b => b.DeviationId)
            .HasColumnName("DeviationID")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(b => b.Deviation)
            .WithMany(b => b.DeviationValues)
            .HasForeignKey(b => b.DeviationId)
            .IsRequired();
    }
}
