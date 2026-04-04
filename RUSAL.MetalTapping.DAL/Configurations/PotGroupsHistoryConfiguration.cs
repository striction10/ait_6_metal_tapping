using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class PotGroupsHistoryConfiguration : IEntityTypeConfiguration<PotGroupsHistoryModel>
    {
        public void Configure(EntityTypeBuilder<PotGroupsHistoryModel> builder)
        {
            builder.ToTable("PotGroupsHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.PotGroupId)
                .HasColumnName("PotGroupId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.PotId)
                .HasColumnName("PotId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(x => x.PotGroup)
                .WithMany(x => x.History)
                .HasForeignKey(x => x.PotGroupId)
                .IsRequired();

            builder.HasOne(x => x.Pot)
                .WithMany(x => x.GroupsHistory)
                .HasForeignKey(x => x.PotId)
                .IsRequired();
        }
    }
}