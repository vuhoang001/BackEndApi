using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEndApi.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [Required] [MaxLength(500)] public required string Name { get; set; }

    [MaxLength(500)] public string? Description { get; set; }

    [Range(1, int.MaxValue)] public decimal Price { get; set; }

    [Range(1, int.MaxValue)] public int Stock { get; set; }

    [Range(1, 5)] public int Rating { get; set; }

    public List<ProductCategory> ProductCategories { get; set; } = new();

    public List<Reviews> Reviews { get; set; }
}

public class ProductCategory
{
    public int ProductId { get; set; }
    [JsonIgnore] public Product Product { get; set; }

    public int CategoryId { get; set; }
    public Categories Category { get; set; }
}

public class ProductDatabaseConfiguration : IEntityTypeConfiguration<Product>, IEntityTypeConfiguration<Categories>,
    IEntityTypeConfiguration<ProductCategory>, IEntityTypeConfiguration<Reviews>

{
    public void Configure(EntityTypeBuilder<Product> ent)
    {
        ent.HasKey(x => x.Id);
        ent.HasIndex(x => x.Name).IsUnique();
    }

    public void Configure(EntityTypeBuilder<Categories> ent)
    {
        ent.HasKey(x => x.Id);
        ent.HasIndex(x => x.Id).IsUnique();
    }


    public void Configure(EntityTypeBuilder<ProductCategory> ent)
    {
        ent.HasKey(x => new { x.ProductId, x.CategoryId });

        ent.HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        ent.HasOne(pc => pc.Category)
            .WithMany()
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Reviews> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.productId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}