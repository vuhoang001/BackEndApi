using AutoMapper;
using BackEndApi.Data;
using BackEndApi.DTO.Request;
using BackEndApi.Exceptions;
using BackEndApi.Models;
using BackEndApi.Models.Common;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<Pagination> GetAllAsync(ParamQuery paramQuery)
    {
        var query = _context.Products
            
            .Include(p => p.ProductCategories) // ✅ Bao gồm bảng trung gian
            .ThenInclude(pc => pc.Category)    // ✅ Bao gồm `Categories`
            .ApplyFiltering(paramQuery)
            .AsNoTracking()
            .AsQueryable();

        var total = await query.CountAsync();
        var result = await query.ApplyOrdering(paramQuery).ApplyPaging(paramQuery).ToListAsync();
        return new Pagination()
        {
            Total = total,
            Page = paramQuery.Page,
            PageSize = paramQuery.PageSize,
            Result = result
        };
    }

    public async Task<Product> CreateAsync(Product_Create productCreate)
    {
        var newProduct = _mapper.Map<Product>(productCreate);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();

        if (productCreate.CategoryIds?.Any() == true)
        {
            var validCategories = await _context.Categories.Where(c => productCreate.CategoryIds.Contains(c.Id))
                .Select(c => c.Id).ToListAsync();

            if (!validCategories.Any())
            {
                await transaction.RollbackAsync();
                throw new BadRequestException("Category doesn't exist");
            }

            var productCategories = validCategories.Select(id => new ProductCategory()
            {
                CategoryId = id,
                ProductId = newProduct.Id
            });

            _context.ProductCategories.AddRange(productCategories);
            await _context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
        return newProduct;
    }

    public async Task<Product> UpdateAsync(int id, Product_Create productCreate)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var product = await _context.Products.FindAsync(id);

        if (product == null) throw new NotFoundException("PRODUCT_NOT_FOUND");

        _mapper.Map(productCreate, product);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        return product;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var product = await _context.Products.FindAsync(id);

        if (product == null) throw new NotFoundException("PRODUCT_NOT_FOUND");

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) throw new NotFoundException("PRODUCT_NOT_FOUND", $"PRODUCT WITH {id} NOT FOUND");

        return product;
    }
}