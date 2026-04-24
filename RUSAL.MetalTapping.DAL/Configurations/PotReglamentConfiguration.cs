using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotReglamentConfiguration : IEntityTypeConfiguration<PotReglamentModel>
{
    public void Configure(EntityTypeBuilder<PotReglamentModel> builder)
    {
        builder.ToTable("PotReglament");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(b => b.ReglamentId)
            .HasColumnType("uniqueidentifier");
        
        builder.Property(b => b.PotId)
            .HasColumnType("uniqueidentifier");
        
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