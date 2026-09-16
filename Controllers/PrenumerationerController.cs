using Microsoft.AspNetCore.Mvc;
using PrenumerationerApi.Models;

namespace PrenumerationerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrenumerationerController : ControllerBase
{
    private static readonly List<Prenumeration> _prenumerationer = new()
    {
        new Prenumeration { Id = 1, ServiceName = "Netflix", Note = "Månadsplan", StartDate = new DateOnly(2025, 1, 1), IsActive = true },
        new Prenumeration { Id = 2, ServiceName = "Spotify", Note = "Årsplan", StartDate = new DateOnly(2024, 6, 1), EndDate = new DateOnly(2025, 6, 1), IsActive = false }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_prenumerationer);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _prenumerationer.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(Prenumeration newItem)
    {
        newItem.Id = _prenumerationer.Count == 0 ? 1 : _prenumerationer.Max(p => p.Id) + 1;
        _prenumerationer.Add(newItem);
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Prenumeration updated)
    {
        var item = _prenumerationer.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();

        item.ServiceName = updated.ServiceName;
        item.Note = updated.Note;
        item.StartDate = updated.StartDate;
        item.EndDate = updated.EndDate;
        item.IsActive = updated.IsActive;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _prenumerationer.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();

        _prenumerationer.Remove(item);
        return NoContent();
    }
}