using BackEndApi.Models;
using BackEndApi.Models.Common;

namespace BackEndApi.Services.Review;

public interface IReviewService
{
    public Task<Pagination> GetAllAsync(ParamQuery paramQuery);
    
    public Task<Reviews> GetByIdAsync(int id);
    
    public Task<Boolean>  DeleteAsync(int id);
    
    public Task<Reviews> UpdateAsync(int id, DTO.Request.Review_create reviewCreate);
    
    public Task<Reviews> CreateAsync(DTO.Request.Review_create reviewCreate);
}