using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.HasKey(p => p.Id);

            // Seller Relationship
            builder.HasOne(e => e.Seller)
                  .WithMany()
                  .HasForeignKey(e => e.SellerId)
                  .OnDelete(DeleteBehavior.NoAction);

            // Buyer Relationship
            builder.HasOne(e => e.Buyer)
                  .WithMany()
                  .HasForeignKey(e => e.BuyerId)
                  .OnDelete(DeleteBehavior.NoAction);

            // OrderDetails relationship
            builder.HasMany(oh => oh.OrderDetails)
                  .WithOne(od => od.OrderHeader)
                  .HasForeignKey(od => od.OrderHeaderId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
