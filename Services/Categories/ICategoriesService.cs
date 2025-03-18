using BackEndApi.Models.Common;

namespace BackEndApi.Services.Categories;

public interface ICategoriesService
{
    public Task<Models.Categories> CreateAsync(DTO.Request.Categories_Create categoryCreate);

    public Task<Boolean> DeleteAsync(int id);
    
    public Task<Models.Categories> UpdateAsync(int id,  DTO.Request.Categories_Create categoriesCreate);
    
    public  Task<Models.Categories> GetByIdAsync(int id);
    
    public Task<Pagination> GetAllAsync(ParamQuery paramQuery);
    
    
    
}