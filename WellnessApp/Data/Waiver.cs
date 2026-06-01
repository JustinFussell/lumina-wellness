namespace Lumina.Data;

public class Waiver
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public DateTime SignedAt { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
    public string? SignatureData { get; set; } // For canvas signature in future
    public string Version { get; set; } = "1.0";
}