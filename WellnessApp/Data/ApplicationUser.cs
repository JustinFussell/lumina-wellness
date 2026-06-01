using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lumina.Data;

/// <summary>
/// Extended Identity user for Lumina members, instructors, and owners.
/// Captures wellness profile + accessibility needs (core to our inclusive mission).
/// </summary>
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [MaxLength(100)]
    public string? FullName { get; set; }

    [PersonalData]
    [MaxLength(20)]
    public string? Pronouns { get; set; }

    /// <summary>
    /// What brings them to Lumina / their goals.
    /// </summary>
    [MaxLength(500)]
    public string? Goals { get; set; }

    /// <summary>
    /// Self-reported accessibility needs, injuries, or preferences.
    /// Kept private and only shown to instructors/admins when relevant.
    /// </summary>
    [MaxLength(1000)]
    public string? AccessibilityNeeds { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<UserCredit> UserCredits { get; set; } = new List<UserCredit>();
    public ICollection<Waiver> Waivers { get; set; } = new List<Waiver>();
}