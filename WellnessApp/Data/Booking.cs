namespace Lumina.Data;

public enum BookingStatus
{
    Reserved,
    Cancelled,
    Attended,
    NoShow
}

public class Booking
{
    public int Id { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int ScheduledClassId { get; set; }
    public ScheduledClass ScheduledClass { get; set; } = null!;

    public BookingStatus Status { get; set; } = BookingStatus.Reserved;

    public int CreditsUsed { get; set; }

    public DateTime BookedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? CancelledAtUtc { get; set; }

    /// <summary>
    /// Member's intention or note for this specific class.
    /// </summary>
    public string? MemberNotes { get; set; }

    /// <summary>
    /// Instructor notes after the class (private).
    /// </summary>
    public string? InstructorNotes { get; set; }
}