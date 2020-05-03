using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderManagementSystem.Models
{
    public partial class navtechContext : DbContext
    {
        public navtechContext()
        {
        }

        public navtechContext(DbContextOptions<navtechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public DbQuery<ResultModel> ResultModel { get; set; }
        public DbQuery<DBStatus> DBStatus { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3PQ70JS\\SQLEXPRESS;Initial Catalog=navtech;Integrated Security=True");
            }
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");
            #region Products
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.PrdId)
                    .HasName("PK__PRODUCTS__7168B164014859AE");

                entity.ToTable("PRODUCTS");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ProductHeight)
                    .HasColumnName("productHeight")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ProductWeight).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Productprice).HasColumnType("decimal(18, 0)");
            });

            #endregion
            #region Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Order__C3905BCF88005FBB");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Modified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.IsDelete)
                .HasColumnType("IsDelete");
            });
            #endregion

            #region Users 
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__Users__C5B19662D95E4EB7");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.Country).HasMaxLength(250);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.State).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(250);
                entity.Property(e => e.Token).HasColumnName("Token");
            });

#endregion
        }
    }
}
