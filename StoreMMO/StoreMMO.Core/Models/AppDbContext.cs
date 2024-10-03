using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<InfoAdd> InfoAdds { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductConnect> ProductConnects { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        public virtual DbSet<StoreDetail> StoreDetails { get; set; }

        public virtual DbSet<StoreType> StoreTypes { get; set; }

        public virtual DbSet<WishList> WishLists { get; set; }
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TRANGIAHUY;Database =StoreMMO;uid=sa;pwd=1035;encrypt=true;trustServerCertificate=true;", b => b.MigrationsAssembly("StoreMMO.Core"));
            }
        }


    }
}
