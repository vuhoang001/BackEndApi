using BackEndApi.Models;
using BackEndApi.Models.Common;
using BackEndApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEndApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] ParamQuery paramQuery)
    {
        var result = await _productService.GetAllAsync(paramQuery);
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] DTO.Request.Product_Create productCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productService.CreateAsync(productCreate);
        return Ok(result);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] DTO.Request.Product_Create productCreate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _productService.UpdateAsync(id, productCreate);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Boolean>> Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _productService.DeleteAsync(id);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        return Ok(result);
    }
}