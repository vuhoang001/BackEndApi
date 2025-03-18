using AutoMapper;
using BackEndApi.Data;
using BackEndApi.DTO.Request;
using BackEndApi.Models.Common;
using Gridify;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Services.Categories;

public class CategoriesService : ICategoriesService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CategoriesService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<Models.Categories> CreateAsync(DTO.Request.Categories_Create categories)
    {
        var newCategory = _mapper.Map<Models.Categories>(categories);
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        _dbContext.Categories.Add(newCategory);
        await _dbContext.SaveChangesAsync();

        await transaction.CommitAsync();

        return newCategory;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Categories> UpdateAsync(int id, Categories_Create categoriesCreate)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Categories> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Pagination> GetAllAsync(ParamQuery paramQuery)
    {
        var query = _dbContext.Categories.ApplyFiltering(paramQuery).AsNoTracking().AsQueryable();


        var total = await query.CountAsync();
        var result = await query.ApplyOrdering(paramQuery).ApplyPaging(paramQuery).ToListAsync();

        return new Pagination()
        {
            Total = total,
            Page = paramQuery.Page, PageSize = paramQuery.PageSize, Result = result
        };
    }
}