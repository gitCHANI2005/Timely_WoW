using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface  IContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Deliver> Delivers { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<MenuDose> MenuDoses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Store> Stores { get; set; }
//        public DbSet<User> Users { get; set; }
        void save();
    }
}
