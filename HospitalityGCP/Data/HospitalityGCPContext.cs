using HospitalityGCP.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalityGCP.Data
{
    public class HospitalityGCPContext : DbContext
    {
        public HospitalityGCPContext(DbContextOptions<HospitalityGCPContext> options) : base(options)
        {
        }

        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<ViewModelOrdersList> ViewModelOrdersList { get; set; }
        public DbSet<ViewModelOrderedItems> ViewModelOrderedItems { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<OrderedItems> OrderedItems {get; set;}
        public DbSet<Rooms>Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItems>().ToTable("tblMenuItems");
            modelBuilder.Entity<SystemUser>().ToTable("tblUsers");
            modelBuilder.Entity<Order>().ToTable("tblOrders");
            modelBuilder.Entity<ViewModelOrdersList>().ToTable("vOrders");
            modelBuilder.Entity<ViewModelOrderedItems>().ToTable("vOrderedItems");
            modelBuilder.Entity<Basket>().ToTable("tblBasket");
            modelBuilder.Entity<OrderedItems>().ToTable("tblOrderedItems");
            modelBuilder.Entity<Rooms>().ToTable("tblRooms");
        }
    }
}
