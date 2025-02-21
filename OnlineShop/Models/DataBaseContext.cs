﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Frameworks;
using OnlineShop.Models.DomainModels.ProductAggregates;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace OnlineShop.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        #region [- OnModelCreating(ModelBuilder modelBuilder) -]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(DatabaseConstants.Schemas.UserManagement);

            #region [- ApplyConfigurationsFromAssembly() -]
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            #region [- RegisterAllEntities() -]
            modelBuilder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        public DbSet<Product> Product { get; set; }
    }
}
