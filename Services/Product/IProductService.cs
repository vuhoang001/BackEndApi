using BackEndApi.Models;
using BackEndApi.Models.Common;

namespace BackEndApi.Services;

public interface IProductService
{
    public Task<Pagination> GetAllAsync(ParamQuery paramQuery);
    public Task<Product> CreateAsync(DTO.Request.Product_Create productCreate);
    public Task<Product> UpdateAsync(int id, DTO.Request.Product_Create productCreate);
    public Task<Boolean> DeleteAsync(int id);
    public Task<Product> GetByIdAsync(int id);
}