using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEndApi.Models;

public class Reviews
{
    [Key] public int Id { get; set; }

    [Range(1, 5)] public int Rating { get; set; }

    [MaxLength(255)] public string? Comment { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    [MaxLength(255)] public string? reviewerName { get; set; }

    [MaxLength(255)] public string? reviewerEmail { get; set; }

    public int productId { get; set; }
    [JsonIgnore] public Product Product { get; set; }
}