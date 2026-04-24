using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUSAL.MetalTapping.DAL.Models;

namespace RUSAL.MetalTapping.DAL.Configurations;

public class OrderConfgiration : IEntityTypeConfiguration<OrderModel>
{
    public void Configure(EntityTypeBuilder<OrderModel> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired();
        
        builder.Property(b => b.WeightOfMetal)
            .HasColumnName("WeightOfMetal")
            .HasColumnType("numeric(10, 0)")
            .IsRequired();
        
        builder.Property(b => b.DateOfOrder)
            .HasColumnName("DateOfOrder")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(b => b.MetalMarkId)
            .HasColumnType("uniqueidentifier");
        
        builder.HasOne(b => b.MetalMark)
            .WithMany(b => b.Orders)
            .HasForeignKey(b => b.MetalMarkId)
            .IsRequired();
    }
}