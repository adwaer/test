using BN.Domain.Features.Products.Models;
using BN.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BN.Dal
{
    public class DbCtx: DbContext
    {
        public DbCtx(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<SimpleMessageResult> SimpleMessageResults { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }
    }
}