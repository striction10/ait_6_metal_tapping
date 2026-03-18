using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class MetalMarkAnalysisConfiguration : IEntityTypeConfiguration<MetalMarkAnalysisModel>
    {
        public void Configure(EntityTypeBuilder<MetalMarkAnalysisModel> builder)
        {
            builder.ToTable("MetalMarkAnalysis");
            
            builder.HasKey(k => k.Id);

            builder.Property(b => b.PotId)
                .HasColumnName("PotId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(b => b.MetalMarkId)
                .HasColumnName("MetalMarkId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(b => b.DateOfReceipt)
                .HasColumnName("DateOfReceipt")
                .HasColumnType("date")
                .IsRequired();

            builder.HasMany(p => p.Values)
                .WithOne(p => p.Analysis)
                .HasForeignKey(p => p.MetalMarkAnalysisId)
                .IsRequired();

            builder.HasOne(p => p.MetalMark)
                .WithMany(p => p.Analyses)
                .HasForeignKey(p => p.MetalMarkId)
                .IsRequired();

            builder.HasOne(p => p.Pot)
                .WithMany(p => p.metalMarkAnalyses)
                .HasForeignKey (p => p.PotId)
                .IsRequired();
        }
    }
}