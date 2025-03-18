using System.ComponentModel.DataAnnotations;

namespace BackEndApi.DTO.Request;

public class Categories_Create
{
    [Required] [MaxLength(255)] public required string CategoryName { get; set; }
    [MaxLength(255)] public string? Description { get; set; }
}