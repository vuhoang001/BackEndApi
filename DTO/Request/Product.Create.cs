using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackEndApi.DTO.Request;

public class Product_Create
{
    [Required] public required string Name { get; set; }

    [MaxLength(500)] public string? Description { get; set; }

    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public decimal Price { get; set; }

    [DefaultValue(0)]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public List<int> CategoryIds { get; set; } = new();
}