using Microsoft.EntityFrameworkCore;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Data;

public class AboutRepository(DataContext context) : IAboutRepository
{
    public async Task<Description?> GetDescriptionAsync()
    {
        return await context.Description.FirstOrDefaultAsync();
    }

    public void UpdateDescription(Description description)
    {
        context.Entry(description).State = EntityState.Modified;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
