using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<BuildingModel>
    {
        public void Configure(EntityTypeBuilder<BuildingModel> builder)
        {
            builder.ToTable("Building");

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
}