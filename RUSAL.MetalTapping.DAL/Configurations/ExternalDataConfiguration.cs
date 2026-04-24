using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class ExternalDataConfiguration : IEntityTypeConfiguration<ExternalDataModel>
{
    public void Configure(EntityTypeBuilder<ExternalDataModel> builder)
    {
        builder.ToTable("ExternalData");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.DateOfReceipt)
            .HasColumnName("DateOfReceipt")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(b => b.PotId)
            .HasColumnType("uniqueidentifier");

        builder.Property(b => b.PotParametersGroupId)
            .HasColumnType("uniqueidentifier");
        
        builder.HasOne(b => b.Pot)
            .WithMany(b => b.ExternalDatas)
            .HasForeignKey(b => b.PotId)
            .IsRequired();

        builder.HasOne(b => b.Parameters)
            .WithMany()
            .HasForeignKey(b => b.PotParametersGroupId)
            .IsRequired();
    }
}