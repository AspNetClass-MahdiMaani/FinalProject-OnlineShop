using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DomainModels.OrderAggregates;

namespace OnlineShop.Models.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable(nameof(OrderHeader)).HasOne(e => e.Seller).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.ToTable(nameof(OrderHeader)).HasOne(e => e.Buyer).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.ToTable(nameof(OrderHeader), "Orders");
            builder.HasKey(p => p.Id);
            //builder.ToTable(nameof(OrderHeader)).HasOne(x => x.Seller).WithMany().OnDelete(DeleteBehavior.NoAction);
            //builder.ToTable(nameof(OrderHeader)).HasOne(e => e.Seller).WithMany().HasForeignKey(e => e.ForeignKeyColumn).OnDelete(DeleteBehavior.NoAction).OnUpdate(UpdateBehavior.NoAction);

            //builder.Property(p => p.FName).IsRequired().HasMaxLength(50);
            //builder.Property(p => p.LName).IsRequired().HasMaxLength(50);
        }
    }
}
