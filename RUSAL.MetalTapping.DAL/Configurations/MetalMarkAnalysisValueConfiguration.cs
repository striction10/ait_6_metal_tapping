using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations{
    public class MetalMarkAnalysisValueConfiguration : IEntityTypeConfiguration<MetalMarkAnalysisValue>
    {
        public void Configure(EntityTypeBuilder<MetalMarkAnalysisValue> builder)
        {
            builder.ToTable("MetalMarkAnalysisValues");
            
            builder.HasKey(k => k.Id);
            
            builder.Property(k => k.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric(37, 0)")
                .IsRequired();
            
            builder.Property(k => k.Value)
                .HasColumnName("Value")
                .HasColumnType("numeric(10, 0)")
                .IsRequired();
            
            builder.Property(k => k.DateOfReceipt)
                .HasColumnName("DateOfReceipt")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(k => k.MetalMarkAnalysisId)
                .HasColumnName("MetalMarkAnalysisID")
                .HasColumnType("numeric(37, 0)")
                .IsRequired();

            builder.Property(k => k.ChemicalElemId)
                .HasColumnType("numeric(37, 0");

            builder.HasOne(k => k.ChemicalElem)
                .WithMany(k => k.Values)
                .HasForeignKey(k => k.ChemicalElemId)
                .IsRequired();
        }
    }
}