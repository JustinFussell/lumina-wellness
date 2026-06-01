using Lumina.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages.Admin;

[Authorize(Roles = "Owner")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public int TodaysClasses { get; set; }
    public int TodaysSpotsLeft { get; set; }
    public int TodayOccupancy { get; set; }
    public int ActiveMembers { get; set; }
    public int CreditsIssued30d { get; set; }

    public List<ScheduledClass> TodaysScheduledClasses { get; set; } = new();
    public List<ApplicationUser> RecentMembers { get; set; } = new();

    public async Task OnGetAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

        // Today's classes
        TodaysScheduledClasses = await _db.ScheduledClasses
            .Include(s => s.ClassType)
            .Include(s => s.Instructor)
            .Include(s => s.Bookings)
            .Where(s => s.StartUtc >= today && s.StartUtc < tomorrow && !s.IsCancelled)
            .OrderBy(s => s.StartUtc)
            .ToListAsync();

        TodaysClasses = TodaysScheduledClasses.Count;

        int totalCapacity = TodaysScheduledClasses.Sum(s => s.EffectiveCapacity);
        int totalBooked = TodaysScheduledClasses.Sum(s => s.Bookings.Count(b => b.Status != BookingStatus.Cancelled));
        TodaysSpotsLeft = Math.Max(0, totalCapacity - totalBooked);
        TodayOccupancy = totalCapacity > 0 ? (int)Math.Round((double)totalBooked / totalCapacity * 100) : 0;

        // Active members (simplified)
        ActiveMembers = await _db.Users
            .Where(u => u.Bookings.Any(b => b.BookedAtUtc >= thirtyDaysAgo) || 
                       u.UserCredits.Any(c => c.PurchasedAt >= thirtyDaysAgo))
            .CountAsync();

        // Credits issued last 30 days (rough proxy)
        CreditsIssued30d = await _db.UserCredits
            .Where(c => c.PurchasedAt >= thirtyDaysAgo)
            .SumAsync(c => (int?)c.CreditsRemaining + (c.Package.Credits - c.CreditsRemaining)) ?? 0;

        // Recent members
        RecentMembers = await _db.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(8)
            .ToListAsync();
    }
}