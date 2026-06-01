namespace Lumina.Data;

public class UserCredit
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int PackageId { get; set; }
    public Package Package { get; set; } = null!;

    public int CreditsRemaining { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
}