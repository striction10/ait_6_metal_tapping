using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class MetalMarkConfiguration : IEntityTypeConfiguration<MetalMark>
{
    public void Configure(EntityTypeBuilder<MetalMark> builder)
    {
        builder.ToTable("MetalMark");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
    }
}
