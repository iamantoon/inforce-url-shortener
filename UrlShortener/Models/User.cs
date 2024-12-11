using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models;

public class User : IdentityUser<int>
{
    public List<Url> Links { get; set; } = [];
}

