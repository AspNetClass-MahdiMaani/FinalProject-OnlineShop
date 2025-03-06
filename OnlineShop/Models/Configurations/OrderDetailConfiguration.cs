using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail", "AppOrder");
            builder.HasKey(od => new
            {
                od.OrderHeaderId,
                od.ProductId,
            });

        }
    }
}
