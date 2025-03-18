using System.ComponentModel.DataAnnotations;

namespace BackEndApi.DTO.Request;

public class Review_create
{
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }

    [MaxLength(255, ErrorMessage = "Comment cannot exceed 255 characters.")]
    public string? Comment { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    [MaxLength(255, ErrorMessage = "Reviewer name cannot exceed 255 characters.")]
    public string? ReviewerName { get; set; }

    [MaxLength(255, ErrorMessage = "Reviewer email cannot exceed 255 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? ReviewerEmail { get; set; }

    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }
}