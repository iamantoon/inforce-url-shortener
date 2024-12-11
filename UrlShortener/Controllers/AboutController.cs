using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers;

public class AboutController(IAboutRepository aboutRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var description = await aboutRepository.GetDescriptionAsync();

        string content = description?.Content ?? "No description available";

        return View("Index", content);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit([FromBody] EditDescriptionDTO description)
    {
        if (description == null || string.IsNullOrEmpty(description.Content))
        {
            return BadRequest("Description content cannot be null or empty");
        }

        var existingDescription = await aboutRepository.GetDescriptionAsync();

        if (existingDescription == null)
        {
            return NotFound("Description not found");
        }

        existingDescription.Content = description.Content;

        aboutRepository.UpdateDescription(existingDescription);
        // context.Entry(existingDescription).State = EntityState.Modified;

        if (await aboutRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the description");
    }
}
