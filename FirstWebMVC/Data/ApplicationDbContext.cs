using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Models;

namespace FirstWebMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // ======================
        // BUỔI 10
        // ======================
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        // ======================
        // CÁC BUỔI TRƯỚC
        // ======================
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // ======================
        // BÀI THỰC HÀNH 12
        // ======================
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<DeviceCategory> DeviceCategories { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<ImportReceipt> ImportReceipts { get; set; }

        public DbSet<ImportReceiptDetail> ImportReceiptDetails { get; set; }

        public DbSet<ExportReceipt> ExportReceipts { get; set; }

        public DbSet<ExportReceiptDetail> ExportReceiptDetails { get; set; }

        // ======================
        // FIX LỖI CASCADE DELETE
        // ======================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ImportReceiptDetail>()
                .HasOne(i => i.Device)
                .WithMany()
                .HasForeignKey(i => i.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExportReceiptDetail>()
                .HasOne(e => e.Device)
                .WithMany()
                .HasForeignKey(e => e.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}