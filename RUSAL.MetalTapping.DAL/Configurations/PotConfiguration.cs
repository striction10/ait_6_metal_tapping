using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

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

            builder.Property(b => b.StateId)
                .HasColumnType("numeric(37, 0)");
            
            builder.Property(b => b.BuildingId)
                .HasColumnType("numeric(37, 0)");
            
            builder.HasOne(b => b.Building)
                .WithMany(b => b.Pots)
                .HasForeignKey(b => b.BuildingId)
                .IsRequired();

            builder.HasOne(b => b.State)
                .WithMany(b => b.Pots)
                .HasForeignKey(b => b.StateId)
                .IsRequired();
        }
    }
}
