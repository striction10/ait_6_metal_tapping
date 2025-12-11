using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotParametersGroupConfiguraion : IEntityTypeConfiguration<PotParametersGroup>
{
    public void Configure(EntityTypeBuilder<PotParametersGroup> builder)
    {
        builder.ToTable("PotParametersGroup");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();
    }
}