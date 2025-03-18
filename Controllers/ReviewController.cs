using BackEndApi.Exceptions;
using BackEndApi.Models;
using BackEndApi.Services.Review;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<ActionResult<Reviews>> Create(DTO.Request.Review_create reviewCreate)
    {
        var result = await _reviewService.CreateAsync(reviewCreate);

        return Ok(result);
    }
}