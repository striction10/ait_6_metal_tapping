using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotParametersGroupConfiguraion : IEntityTypeConfiguration<PotParametersGroupModel>
{
    public void Configure(EntityTypeBuilder<PotParametersGroupModel> builder)
    {
        builder.ToTable("PotParametersGroup");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
    }
}