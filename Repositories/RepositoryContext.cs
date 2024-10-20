using HerSeyVar.Models;
using Microsoft.EntityFrameworkCore;

namespace HerSeyVar.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        // Ürünler için DbSet
        public DbSet<Product> Products { get; set; }
        // Kategoriler için DbSet
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kategoriler için başlangıç verisi ekleme
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothing" }
            );

            // Ürünler için başlangıç verisi ekleme
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone", Price = 999, Stock = 50, CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Description = "High performance laptop", Price = 1500, Stock = 30, CategoryId = 1 },
                new Product { Id = 3, Name = "Novel", Description = "Bestselling novel", Price = 1900, Stock = 200, CategoryId = 2 },
                new Product { Id = 4, Name = "T-Shirt", Description = "Cotton T-shirt", Price = 2500, Stock = 100, CategoryId = 3 }
            );
        }
    }
}
