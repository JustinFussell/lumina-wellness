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
    public int CurrentStreak { get; set; }
    public Booking? NextBooking { get; set; }
    public List<Booking> UpcomingBookings { get; set; } = new();
    public List<Booking> RecentHistory { get; set; } = new(); // last attended for beautiful timeline

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
            .Take(4)
            .ToListAsync();

        NextBooking = UpcomingBookings.FirstOrDefault();

        // Beautiful practice history (last 8 attended)
        RecentHistory = await _db.Bookings
            .Include(b => b.ScheduledClass)
                .ThenInclude(s => s.ClassType)
            .Include(b => b.ScheduledClass)
                .ThenInclude(s => s.Instructor)
            .Where(b => b.UserId == userId && b.Status == BookingStatus.Attended)
            .OrderByDescending(b => b.ScheduledClass.StartUtc)
            .Take(8)
            .ToListAsync();

        // Calculate real consecutive day streak (attended classes on consecutive calendar days)
        CurrentStreak = CalculateStreak(userId);
    }

    private int CalculateStreak(string userId)
    {
        var attendedDates = _db.Bookings
            .Include(b => b.ScheduledClass)
            .Where(b => b.UserId == userId && b.Status == BookingStatus.Attended)
            .Select(b => b.ScheduledClass.StartUtc.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToList();

        if (!attendedDates.Any()) return 0;

        int streak = 1;
        var current = attendedDates[0];

        for (int i = 1; i < attendedDates.Count; i++)
        {
            if (current.AddDays(-1) == attendedDates[i])
            {
                streak++;
                current = attendedDates[i];
            }
            else
            {
                break;
            }
        }
        return streak;
    }
}