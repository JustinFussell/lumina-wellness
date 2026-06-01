namespace Lumina.Data;

public class Lead
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Interest { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsContacted { get; set; }
}