using Microsoft.AspNetCore.Mvc;
using PrenumerationerApi.Models;
using PrenumerationerApi.Services;

namespace PrenumerationerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrenumerationerController : ControllerBase
{
    private const long MaxFileSize = 25 * 1024 * 1024;
    private static readonly string[] LogoExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
    private static readonly string[] DocumentExtensions = [".pdf", ".jpg", ".jpeg", ".png", ".webp"];

    private static readonly List<Prenumeration> _prenumerationer = new()
    {
        new Prenumeration { Id = 1, ServiceName = "Netflix", Note = "Månadsplan", StartDate = new DateOnly(2025, 1, 1), IsActive = true },
        new Prenumeration { Id = 2, ServiceName = "Spotify", Note = "Årsplan", StartDate = new DateOnly(2024, 6, 1), EndDate = new DateOnly(2025, 6, 1), IsActive = false }
    };

    private readonly FileStorage _fileStorage;

    public PrenumerationerController(FileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

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

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo(int id, IFormFile file)
    {
        var item = _prenumerationer.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();

        var error = ValidateFile(file, LogoExtensions);
        if (error is not null) return BadRequest(error);

        item.LogoUrl = await _fileStorage.SaveAsync(file);
        return Ok(item);
    }

    [HttpPost("{id}/document")]
    public async Task<IActionResult> UploadDocument(int id, IFormFile file)
    {
        var item = _prenumerationer.FirstOrDefault(p => p.Id == id);
        if (item is null) return NotFound();

        var error = ValidateFile(file, DocumentExtensions);
        if (error is not null) return BadRequest(error);

        item.DocumentUrl = await _fileStorage.SaveAsync(file);
        item.DocumentName = Path.GetFileName(file.FileName);
        return Ok(item);
    }

    private static string? ValidateFile(IFormFile file, string[] allowedExtensions)
    {
        if (file.Length == 0) return "Filen är tom.";
        if (file.Length > MaxFileSize) return $"Filen får vara högst {MaxFileSize / 1024 / 1024} MB.";

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension)) return $"Tillåtna filtyper: {string.Join(", ", allowedExtensions)}.";

        return null;
    }
}