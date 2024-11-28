using MeShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MeShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>()
                .HasIndex(a => a.Name).IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<User_Address>(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne<Cart>(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.Cart_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                .HasMany<Cart_Item>(c => c.Cart_Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.Cart_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart_Item>()
                .HasKey(ci => new {ci.ProductSize_Id, ci.Cart_Id});
            

            modelBuilder.Entity<Product>()
                .HasMany<Product_Size>(p => p.Product_Sizes)
                .WithOne(pz => pz.Product)
                .HasForeignKey(pz => pz.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany<Image>(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.Product_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product_Size>()
                .HasKey(ps => ps.Id);
            modelBuilder.Entity<Product_Size>()
                .HasIndex(ps => new {ps.Size_Id, ps.Product_Id})
                .IsUnique();

            modelBuilder.Entity<Product_Size>()
                .HasOne<Product_Inventory>(ps => ps.Product_Inventory)
                .WithOne(pi => pi.Product_Size)
                .HasForeignKey<Product_Size>(ps => ps.ProductInventory_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product_Size>()
                .HasMany<Cart_Item>(ps => ps.Cart_Items)
                .WithOne(c => c.Product_Size)
                .HasForeignKey(ci => ci.ProductSize_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Size>()
                .HasMany<Product_Size>(s => s.Product_Sizes)
                .WithOne(pz => pz.Size)
                .HasForeignKey(pz => pz.Size_Id);

            modelBuilder.Entity<Product_Category>()
                .HasMany<Product>(pc => pc.Products)
                .WithOne(p => p.Product_Category)
                .HasForeignKey(p => p.ProductCategory_Id);

            modelBuilder.Entity<Discount>()
               .HasMany<Product>(d => d.Products)
               .WithOne(p => p.Discount)
               .HasForeignKey(p => p.Discount_Id);
           
            modelBuilder.Entity<Order_Detail>()
               .HasOne<Order>(od => od.Order)
               .WithMany(o => o.Orders_Detail)
               .HasForeignKey(o => o.Order_Id);

            modelBuilder.Entity<Order_Detail>()
               .HasOne<Product_Size>(od => od.Product_Size)
               .WithMany(p => p.Orders_Detail)
               .HasForeignKey(od => od.Product_Size_Id);

            modelBuilder.Entity<Order>()
               .HasOne<User>(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.User_Id);

            modelBuilder.Entity<Blog>()
                .HasOne<Image>(b => b.Image)
                .WithOne(i => i.Blog)
                .HasForeignKey<Blog>(b => b.Image_Id);
        }

        public DbSet<AdminUser> Admin_Users { get; set; }
        public DbSet<Cart> Carts{ get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Category> Product_Categories { get; set; }
        public DbSet<Product_Inventory> Products_Inventory { get; set; }
        public DbSet<Product_Size> Products_Size { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Address> User_Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Detail> Order_Details { get; set; }
        public DbSet<Payment_Method> Payment_Methods { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
