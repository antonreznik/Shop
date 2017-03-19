using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;
using DomainModel.Classes.Colors;

namespace DomainModel.Entity
{
    public class EFContext:DbContext
    {
        public DbSet<Parfum> Parfums { get; set; }
        public DbSet<Cosmetic> Cosmetics { get; set; }
        public DbSet<Care> Cares { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductsInOrder> ProductsInOrder { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<DataForPriceBuilding> DataForPrice { get; set; }

        //public EFContext(): base("DB_9CFFAE_RealShop") { }

        //public EFContext() : base("RealshopTestt") { }

        public EFContext() : base("DefaultConnection") { }
    }
}
