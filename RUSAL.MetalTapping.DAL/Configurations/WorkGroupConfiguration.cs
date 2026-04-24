using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;
namespace RUSAL.MetalTapping.DAL.Configurations;

public class WorkGroupConfiguration : IEntityTypeConfiguration<WorkGroupModel>
{
    public void Configure(EntityTypeBuilder<WorkGroupModel> builder)
    {
        builder.ToTable("WorkGroup");
        
        builder.HasKey(wg => wg.Id);
        
        builder.Property(wg => wg.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(wg => wg.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(20)")
            .IsRequired();
    }
}