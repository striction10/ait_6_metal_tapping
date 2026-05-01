using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Entities;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class ShiftTaskConfiguration : IEntityTypeConfiguration<ShiftTask>
{
    public void Configure(EntityTypeBuilder<ShiftTask> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.TapTaskId)
            .HasColumnName("TapTaskId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.ShiftId)
            .HasColumnName("ShiftId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.LeadTime)
            .HasColumnName("LeadTime")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasOne(x => x.Shift)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ShiftId)
            .IsRequired();

        builder.HasOne(x => x.TapTask)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.TapTaskId)
            .IsRequired();
    }
}