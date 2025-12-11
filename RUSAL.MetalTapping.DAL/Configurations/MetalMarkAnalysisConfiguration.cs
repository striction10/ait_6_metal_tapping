using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class MetalMarkAnalysisConfiguration : IEntityTypeConfiguration<MetalMarkAnalysis>
    {
        public void Configure(EntityTypeBuilder<MetalMarkAnalysis> builder)
        {
            builder.ToTable("MetalMarkAnalysis");
            
            builder.HasKey(k => k.Id);

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.HasMany(p => p.Values)
                .WithOne(p => p.Analysis)
                .HasForeignKey(p => p.MetalMarkAnalysisId)
                .IsRequired();
        }
    }
}