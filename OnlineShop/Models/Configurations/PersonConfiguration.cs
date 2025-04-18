﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.DomainModels.OrderAggregates;
using OnlineShop.Models.DomainModels.personAggregates;

namespace OnlineShop.Models.Configurations
{
    public class PersonConfiguration: IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LName).IsRequired().HasMaxLength(50);
        }
    }
}
