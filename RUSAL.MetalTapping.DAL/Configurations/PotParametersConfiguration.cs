using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotParametersConfiguration : IEntityTypeConfiguration<PotParameterModel>
{
    public void Configure(EntityTypeBuilder<PotParameterModel> builder)
    {
        builder.ToTable("PotParameters");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(b => b.Value)
            .HasColumnName("Value")
            .HasColumnType("numeric(10, 0)")
            .IsRequired();
        
        builder.Property(b => b.PotParametersGroupId)
            .HasColumnType("uniqueidentifier");

        builder.HasOne(b => b.Group)
            .WithMany(b => b.Parameters)
            .HasForeignKey(b => b.PotParametersGroupId)
            .IsRequired();
    }
}