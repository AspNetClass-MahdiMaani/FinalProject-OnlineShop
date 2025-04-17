using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new {od.OrderHeaderId,od.ProductId});
            builder.Property(od => od.UnitPrice).IsRequired();
            builder.Property(od => od.Amount).IsRequired();

            builder.HasOne(oh => oh.OrderHeader)
                   .WithMany(od => od.OrderDetails)
                   .HasForeignKey(oh => oh.OrderHeaderId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(od => od.Product)
                   .WithMany(p => p.OrderDetail)
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
