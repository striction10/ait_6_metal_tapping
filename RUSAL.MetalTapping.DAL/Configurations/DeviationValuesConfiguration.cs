using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class DeviationValuesConfiguration : IEntityTypeConfiguration<DeviationValues>
    {
        public void Configure(EntityTypeBuilder<DeviationValues> builder)
        {
            builder.ToTable("DeviationValues");

            builder.Property(b => b.Value)
                .HasColumnName("Value")
                .HasColumnType("numeric(4, 0)")
                .IsRequired();

            builder.Property(b => b.CastingRatio)
                .HasColumnName("CastingRatio")
                .HasColumnType("numeric(4, 0)")
                .IsRequired();

            builder.Property(b => b.DeviationId)
                .HasColumnName("DeviationID")
                .HasColumnType("numeric(37, 0)")
                .IsRequired();

            builder.HasOne(b => b.Deviation)
                .WithMany(b => b.DeviationValues)
                .HasForeignKey(b => b.DeviationId)
                .IsRequired();
        }
    }
}
