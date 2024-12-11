using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Models;

namespace UrlShortener.Data;

public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public required DbSet<Url> Urls { get; set; }
    public required DbSet<Description> Description { get; set; }
}
