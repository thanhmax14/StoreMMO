using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<FeedBack> FeedBacks { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductType> ProductTypes { get; set; }

        public virtual DbSet<ProductConnect> ProductConnects { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        public virtual DbSet<StoreDetail> StoreDetails { get; set; }

        public virtual DbSet<StoreType> StoreTypes { get; set; }

        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<OrderBuy> OrderBuys { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.Entity<Store>()
           .HasOne(s => s.User)  
           .WithMany()  
           .HasForeignKey(s => s.UserId) 
           .OnDelete(DeleteBehavior.Cascade);


          
            builder.Entity<FeedBack>()
                .HasOne(fb => fb.User)  
                .WithMany()  
                .HasForeignKey(fb => fb.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<WishList>()
           .HasOne(s => s.User)
           .WithMany()
           .HasForeignKey(s => s.UserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cart>()
          .HasOne(s => s.User)
          .WithMany()
          .HasForeignKey(s => s.UserId)
          .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Balance>()
       .HasOne(s => s.User)
       .WithMany()
       .HasForeignKey(s => s.UserId)
       .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<OrderBuy>()
               .HasOne(o => o.AppUser) // Assuming AppUser is a navigation property to User
               .WithMany() // Assuming no reverse navigation
               .HasForeignKey(o => o.UserID)
               .OnDelete(DeleteBehavior.Restrict);

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
                string jsonFilePath = @"D:\connectionConfig.json";

                // Đọc file JSON và xây dựng cấu hình
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(jsonFilePath))
                    .AddJsonFile(Path.GetFileName(jsonFilePath))
                    .Build();

                // Lấy chuỗi kết nối từ file JSON
                string connectionString = config.GetConnectionString("df");
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("StoreMMO.Core"));
            }
        }


    }
}
