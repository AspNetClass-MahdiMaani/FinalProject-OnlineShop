using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeader");
            builder.HasKey(p => p.Id);

            builder.HasOne(e => e.Seller) 
              .WithMany()
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Buyer)
                   .WithMany() 
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasMany(oh => oh.OrderDetail)
            //       .WithOne(od => od.OrderHeader) 
            //       .HasForeignKey(od => od.OrderHeaderId) 
            //       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
