using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;

public class UrlController(IUrlsRepository urlsRepository) : Controller
{
    [Authorize]
    public async Task<IActionResult> ShortUrlInfo(int id)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url == null) return NotFound();

        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
        {
            return Unauthorized("Anonymous can't access this page");
        }

        return View(url);
    }
}
