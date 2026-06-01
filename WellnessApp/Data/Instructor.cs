namespace Lumina.Data;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Specialties { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ScheduledClass> ScheduledClasses { get; set; } = new List<ScheduledClass>();
}