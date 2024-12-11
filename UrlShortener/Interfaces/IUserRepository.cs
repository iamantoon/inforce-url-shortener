using UrlShortener.Models;

namespace UrlShortener.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
}
