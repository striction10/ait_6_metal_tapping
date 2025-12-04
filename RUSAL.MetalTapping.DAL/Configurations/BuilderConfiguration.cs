using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class BuilderConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Building");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric(37, 0)")
                .IsRequired();

            builder.Property(b => b.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(20)")
                .IsRequired();
        }
    }
}