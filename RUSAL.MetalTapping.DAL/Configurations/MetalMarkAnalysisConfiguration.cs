using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class MetalMarkAnalysisConfiguration : IEntityTypeConfiguration<MetalMarkAnalysis>
    {
        public void Configure(EntityTypeBuilder<MetalMarkAnalysis> builder)
        {

        }
    }
}
