using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class ExternalDataConfiguration : IEntityTypeConfiguration<ExternalData>
    {
        public void Configure(EntityTypeBuilder<ExternalData> builder)
        {
            builder.ToTable("ExternalData");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.DateOfReceipt)
                .HasColumnName("DateOfReceipt")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(b => b.PotId)
                .HasColumnType("numeric(37, 0)");

            builder.Property(b => b.PotParametersGroupId)
                .HasColumnType("numeric(37, 0)");
            
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
}