using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class PotConfiguration : IEntityTypeConfiguration<Pot>
    {
        public void Configure(EntityTypeBuilder<Pot> builder)
        {
            builder.ToTable("Pot");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric(37, 0)")
                .IsRequired();
        }
    }
}
