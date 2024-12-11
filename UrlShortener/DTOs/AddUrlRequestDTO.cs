using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs;

public class AddUrlRequestDTO
{
    [Url]
    public required string OriginalUrl { get; set; }
}
