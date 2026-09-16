using System.ComponentModel.DataAnnotations;

namespace PrenumerationerApi.Models;

public class Prenumeration
{
    public int Id { get; set; }

    [Required]
    public string ServiceName { get; set; } = "";

    public string? Note { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsActive { get; set; }
}
