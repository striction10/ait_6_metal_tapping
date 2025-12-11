using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Shift");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .HasColumnType("numeric(37, 0)")
            .IsRequired();
        
        builder.Property(b => b.BeginDate)
            .HasColumnName("BeginDate")
            .HasColumnType("datetime")
            .IsRequired();
        
        builder.Property(b => b.EndDate)
            .HasColumnName("EndDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.WorkGroupId)
            .HasColumnType("numeric(37, 0)");
        
        builder.HasOne(b => b.WorkGroup)
            .WithMany(b => b.Shifts)
            .HasForeignKey(b => b.WorkGroupId)
            .IsRequired();
    }
}