using UrlShortener.Models;

namespace UrlShortener.Interfaces;

public interface IAboutRepository
{
    Task<Description?> GetDescriptionAsync();
    void UpdateDescription(Description description);
    Task<bool> SaveChangesAsync();
}
