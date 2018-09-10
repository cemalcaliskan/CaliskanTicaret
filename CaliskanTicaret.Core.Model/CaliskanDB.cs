using CaliskanTicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliskanTicaret.Core.Model
{
    public class CaliskanDB : DbContext
    {
        public CaliskanDB()
            :base(@"Data Source=CEMALCALISKAN;Initial Catalog=CaliskanTicaretDB;Integrated Security=True")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> Adresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderPayment> GetOrderPayments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
 
}
