namespace Lumina.Data;

/// <summary>
/// Private notes by instructors or owners about a member (e.g. injuries, preferences).
/// </summary>
public class MemberNote
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public string Note { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}