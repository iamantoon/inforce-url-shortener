namespace UrlShortener.DTOs;

public class UrlDTO
{
    public int Id { get; set; }
    public required string OriginalUrl { get; set; }
    public required string ShortUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CreatedByUserName { get; set; }
}
