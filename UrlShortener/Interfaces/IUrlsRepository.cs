using UrlShortener.DTOs;
using UrlShortener.Models;

namespace UrlShortener.Interfaces;

public interface IUrlsRepository
{
    Task<List<UrlDTO>> GetUrlsAsync();
    Task<Url?> GetUrlByIdAsync(int id);
    void AddUrl(Url url);
    void DeleteUrl(Url url);
    Task DeleteUrlsAsync();
    Task<Url?> GetUrlByShortUrl(string shortUrlToken);
    Task<Url?> GetUrlByOriginalUrl(string originalUrl);
    Task<bool> SaveChangesAsync();
}
