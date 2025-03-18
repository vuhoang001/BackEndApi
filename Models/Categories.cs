using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackEndApi.Models;

public class Categories
{
    [Key] public int Id { get; set; }

    [MaxLength(50)] public required string CategoryName { get; set; }

    [MaxLength(50)] public string? Description { get; set; }
}