using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class ReglamentConfiguration : IEntityTypeConfiguration<Reglament>
{
    public void Configure(EntityTypeBuilder<Reglament> builder)
    {
        builder.ToTable("Reglament");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(b => b.DateStart)
            .HasColumnName("DateStart")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.DateStop)
            .HasColumnName("DateStop")
            .HasColumnType("datetime")
            .IsRequired();
    }
}