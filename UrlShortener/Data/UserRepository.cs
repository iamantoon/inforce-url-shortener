using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Data;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }
}
