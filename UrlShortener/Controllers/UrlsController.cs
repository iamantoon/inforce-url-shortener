using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlsController(IUrlsRepository urlsRepository, IUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UrlDTO>>> GetUrls()
    {
        return await urlsRepository.GetUrlsAsync();
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<Url>> GetUrl(int id)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url == null) return NotFound(); 

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdValue == null) return Unauthorized(); 

        var userId = int.Parse(userIdValue);

        if (!User.IsInRole("Admin") && url.CreatedById != userId) return Forbid();
        
        return Ok(url);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Url>> AddUrl(AddUrlRequestDTO urlRequest)
    {
        if (string.IsNullOrWhiteSpace(urlRequest.OriginalUrl))
        {
            return BadRequest("Original URL cannot be empty");
        }

        var existingUrl = await urlsRepository.GetUrlByOriginalUrl(urlRequest.OriginalUrl);

        if (existingUrl != null) return Conflict("This URL already exists");

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdValue == null) return Unauthorized();
        
        var userId = int.Parse(userIdValue);

        var shortUrlToken = GenerateShortUrl(urlRequest.OriginalUrl);

        var shortUrl = $"http://localhost:5000/api/urls/{shortUrlToken}";

        var createdBy = await userRepository.GetUserByIdAsync(userId);

        if (createdBy == null) return Unauthorized();

        var url = new Url
        {
            OriginalUrl = urlRequest.OriginalUrl,
            ShortUrl = shortUrl,
            CreatedAt = DateTime.UtcNow,
            CreatedById = userId,
            CreatedBy = createdBy
        };

        urlsRepository.AddUrl(url);

        await urlsRepository.SaveChangesAsync();

        if (url.CreatedBy.UserName == null) return Unauthorized();

        var urlDTO = new UrlDTO
        {
            Id = url.Id,
            OriginalUrl = url.OriginalUrl,
            ShortUrl = url.ShortUrl,
            CreatedAt = url.CreatedAt,
            CreatedByUserName = url.CreatedBy.UserName
        };

        return CreatedAtAction(nameof(GetUrl), new { id = url.Id }, urlDTO);
    }

    [HttpGet("{shortUrlToken}")]
    public async Task<IActionResult> RedirectToOriginal(string shortUrlToken)
    {
        var url = await urlsRepository.GetUrlByShortUrl(shortUrlToken);

        if (url == null)
        {
            return NotFound("Short URL not found");
        }

        return Redirect(url.OriginalUrl);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl(int id)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url == null) return NotFound();

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdValue == null) return Unauthorized();
        
        var userId = int.Parse(userIdValue);

        if (!User.IsInRole("Admin") && url.CreatedById != userId) return Forbid();

        urlsRepository.DeleteUrl(url);

        if (await urlsRepository.SaveChangesAsync())
        {
            return NoContent();
        }
        
        return BadRequest("Problem deleting this url");
    }

    [HttpDelete("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAllUrls()
    {
        await urlsRepository.DeleteUrlsAsync();

        if (await urlsRepository.SaveChangesAsync()) return NoContent();

        return BadRequest("Problem deleting urls");
    }

    private string GenerateShortUrl(string originalUrl)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(originalUrl));
        return Convert.ToBase64String(hashBytes)[..8].Replace("/", "_").Replace("+", "-");
    }
}
