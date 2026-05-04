using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotParametersGroupConfiguraion : IEntityTypeConfiguration<PotParametersGroup>
{
    public void Configure(EntityTypeBuilder<PotParametersGroup> builder)
    {
        builder.ToTable("PotParametersGroup");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
    }
}
