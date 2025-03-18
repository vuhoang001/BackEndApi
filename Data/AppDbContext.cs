using BackEndApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1, Name = "Laptop", Description = "Gaming Laptop", Price = 1000, Stock = 10, Rating = 3
            },
            new Product { Id = 2, Name = "Phone", Description = "Smartphone", Price = 500, Stock = 20, Rating = 5 }
        );
    }
}