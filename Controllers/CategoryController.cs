using BackEndApi.Models;
using BackEndApi.Models.Common;
using BackEndApi.Services.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoriesService _categoryService;

    public CategoryController(ICategoriesService categoriesService)
    {
        _categoryService = categoriesService;
    }


    [HttpPost]
    public async Task<ActionResult<Categories>> Create([FromBody] DTO.Request.Categories_Create categoryCreate)
    {
        var result = await _categoryService.CreateAsync(categoryCreate);

        return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult<Pagination>> Get([FromQuery] ParamQuery paramQuery)
    {
        var result = await _categoryService.GetAllAsync(paramQuery);
        return Ok(result);
    }
}