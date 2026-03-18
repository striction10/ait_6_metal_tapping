using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations
{
    public class CalculatedTaskConfiguration : IEntityTypeConfiguration<CalculatedTaskModel>
    {
        public void Configure(EntityTypeBuilder<CalculatedTaskModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.PotId)
                .HasColumnName("PotId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.CalculatedTaskForPot)
                .HasColumnName("CalculatedTask")
                .HasColumnType("numeric(18, 4)")
                .IsRequired();

            builder.Property(x => x.RoundCalculatedTaskForPot)
                .HasColumnName("RoundCalculatedTask")
                .HasColumnType("numeric(18, 4)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(x => x.Pot)
                .WithMany(x => x.calculatedTasks)
                .HasForeignKey(x => x.PotId)
                .IsRequired();
        }
    }
}