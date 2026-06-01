namespace Lumina.Data;

/// <summary>
/// A specific instance of a class that can be booked.
/// </summary>
public class ScheduledClass
{
    public int Id { get; set; }
    
    public int ClassTypeId { get; set; }
    public ClassType ClassType { get; set; } = null!;

    public int InstructorId { get; set; }
    public Instructor Instructor { get; set; } = null!;

    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }

    public string Room { get; set; } = "Main Studio";
    public int? CapacityOverride { get; set; }

    public string? Notes { get; set; }
    public bool IsCancelled { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public int EffectiveCapacity => CapacityOverride ?? ClassType?.DefaultCapacity ?? 12;
}