using Lumina.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages.App;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public DashboardModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public int ThisMonthClasses { get; set; }
    public int CreditsRemaining { get; set; }
    public Booking? NextBooking { get; set; }
    public List<Booking> UpcomingBookings { get; set; } = new();

    public async Task OnGetAsync()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return;

        var now = DateTime.UtcNow;

        ThisMonthClasses = await _db.Bookings
            .Where(b => b.UserId == userId && b.Status == BookingStatus.Attended && b.ScheduledClass.StartUtc.Month == now.Month)
            .CountAsync();

        CreditsRemaining = await _db.UserCredits
            .Where(c => c.UserId == userId && (c.ExpiresAt == null || c.ExpiresAt > now))
            .SumAsync(c => (int?)c.CreditsRemaining) ?? 0;

        UpcomingBookings = await _db.Bookings
            .Include(b => b.ScheduledClass)
                .ThenInclude(s => s.ClassType)
            .Include(b => b.ScheduledClass)
                .ThenInclude(s => s.Instructor)
            .Where(b => b.UserId == userId && b.Status == BookingStatus.Reserved && b.ScheduledClass.StartUtc > now)
            .OrderBy(b => b.ScheduledClass.StartUtc)
            .Take(5)
            .ToListAsync();

        NextBooking = UpcomingBookings.FirstOrDefault();
    }
}