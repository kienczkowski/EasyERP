using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EasyERP.Models;

namespace EasyERP.Context
{
    public class DBContext : DbContext
    {
        public DBContext() : base(Global.Global.connectionStringName) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<LogError> LogError { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasMany<Product>(s => s.Products).WithMany(c => c.Orders).Map(c =>
            {
                c.MapLeftKey("OrderId");
                c.MapRightKey("ProductId");
                c.ToTable("OrdersProducts");
            });
            base.OnModelCreating(modelBuilder);
        } 
    }
}