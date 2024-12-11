using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models;

public class Url
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The OriginalUrl field is required.")]
    [Url(ErrorMessage = "The OriginalUrl field is not a valid URL.")]
    [StringLength(2048, ErrorMessage = "The URL is too long.")]
    public required string OriginalUrl { get; set; }

    [Required]
    public required string ShortUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int CreatedById { get; set; }

    [Required]
    public required User CreatedBy { get; set; }
}
