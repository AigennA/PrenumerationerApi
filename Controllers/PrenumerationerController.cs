using Microsoft.AspNetCore.Mvc;
using PrenumerationerApi.Models;

[ApiController]
[Route("api/[controller]")]
public class PrenumerationerController : ControllerBase
{
    private static List<Prenumeration> _prenumerationer = new();

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
        item.IsActive = updated.IsActive;
        return NoContent();
    }
}