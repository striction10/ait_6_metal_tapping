using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class PotGroupConfiguration : IEntityTypeConfiguration<PotGroupModel>
{
    public void Configure(EntityTypeBuilder<PotGroupModel> builder)
    {
        builder.ToTable("PotGroup");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.BuildingId)
            .HasColumnName("BuildingId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.ScoopId)
            .HasColumnName("ScoopId")
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.HasOne(x => x.Building)
            .WithMany(x => x.PotGroupModels)
            .HasForeignKey(x => x.BuildingId)
            .IsRequired();

        builder.HasOne(x => x.Scoop)
            .WithMany()
            .HasForeignKey(x => x.ScoopId)
            .IsRequired();

        builder.HasMany(x => x.History)
            .WithOne(x => x.PotGroup)
            .HasForeignKey(x => x.PotGroupId)
            .IsRequired();
    }
}
