using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotReglamentConfiguration : IEntityTypeConfiguration<PotReglament>
{
    public void Configure(EntityTypeBuilder<PotReglament> builder)
    {
        builder.ToTable("PotReglament");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();

        builder.Property(b => b.ReglamentId)
            .HasColumnType("numeric(37, 0)");
        
        builder.Property(b => b.PotId)
            .HasColumnType("numeric(37, 0)");
        
        builder.HasOne(b => b.Reglament)
            .WithMany(b => b.Reglaments)
            .HasForeignKey(b => b.ReglamentId)
            .IsRequired();

        builder.HasOne(b => b.Pot)
            .WithMany(b => b.Reglaments)
            .HasForeignKey(b => b.PotId)
            .IsRequired();
    }
}