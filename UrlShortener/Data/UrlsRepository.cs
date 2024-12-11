using Microsoft.EntityFrameworkCore;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Data;

public class UrlsRepository(DataContext context) : IUrlsRepository
{
    public async Task<List<UrlDTO>> GetUrlsAsync()
    {
        var urls = await context.Urls.Include(u => u.CreatedBy).ToListAsync();

        return urls.Select(url => new UrlDTO
        {
            Id = url.Id,
            OriginalUrl = url.OriginalUrl,
            ShortUrl = url.ShortUrl,
            CreatedAt = url.CreatedAt,
            CreatedByUserName = url.CreatedBy.UserName
        }).ToList();
    }

    public void AddUrl(Url url)
    {
        context.Urls.Add(url);
    }

    public void DeleteUrl(Url url)
    {
        context.Urls.Remove(url);
    }

    public async Task DeleteUrlsAsync()
    {
        var urls = await context.Urls.ToListAsync();

        context.Urls.RemoveRange(urls);
    }

    public async Task<Url?> GetUrlByIdAsync(int id)
    {
        return await context.Urls.Include(u => u.CreatedBy).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Url?> GetUrlByShortUrl(string shortUrlToken)
    {
        var shortUrl = $"http://localhost:5000/api/urls/{shortUrlToken}";

        return await context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
    }

    public async Task<Url?> GetUrlByOriginalUrl(string originalUrl)
    {
        return await context.Urls.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
