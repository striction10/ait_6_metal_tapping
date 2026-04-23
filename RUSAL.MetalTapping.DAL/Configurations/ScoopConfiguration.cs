using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class ScoopConfiguration : IEntityTypeConfiguration<ScoopModel>
{
    public void Configure(EntityTypeBuilder<ScoopModel> builder)
    {
        builder.ToTable("Scoop");
        
        builder.HasKey(scoop => scoop.Id);
        
        builder.Property(scoop => scoop.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(scoop => scoop.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();

        builder.Property(b => b.StateId)
            .HasColumnType("uniqueidentifier");
        
        builder.HasOne(b => b.ScoopState)
            .WithMany(b => b.Scoops)
            .HasForeignKey(b => b.StateId)
            .IsRequired();
    }
}