using AutoMapper;
using BackEndApi.Data;
using BackEndApi.DTO.Request;
using BackEndApi.Exceptions;
using BackEndApi.Models;
using BackEndApi.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Services.Review;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(AppDbContext _context, IMapper _mapper)
    {
        this._context = _context;
        this._mapper = _mapper;
    }

    public async Task<Reviews> CreateAsync(Review_create reviewCreate)
    {
        var review = _mapper.Map<Reviews>(reviewCreate);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == review.productId);

        if (product == null) throw new NotFoundException("PRODUCT_NOT_FOUND", review.productId.ToString());

        review.productId = product.Id;

        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Reviews.Add(review);
        
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return review;
    }

    public Task<Pagination> GetAllAsync(ParamQuery paramQuery)
    {
        throw new NotImplementedException();
    }

    public Task<Reviews> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Reviews> UpdateAsync(int id, Review_create reviewCreate)
    {
        throw new NotImplementedException();
    }
}