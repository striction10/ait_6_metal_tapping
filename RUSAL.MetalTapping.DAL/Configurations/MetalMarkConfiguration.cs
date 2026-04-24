using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class MetalMarkConfiguration : IEntityTypeConfiguration<MetalMarkModel>
{
    public void Configure(EntityTypeBuilder<MetalMarkModel> builder)
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
