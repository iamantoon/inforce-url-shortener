using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [AllowAnonymous]
    public ActionResult<User> GetCurrentUser()
    {
        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Ok(null);
        }

        return Ok(new
        {
            UserName = User.Identity.Name,
            IsAdmin = User.IsInRole("Admin")
        });
    }
}
