using Lumina.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages;

public class ScheduleModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ScheduleModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public List<ScheduledClass> UpcomingClasses { get; set; } = new();

    public async Task OnGetAsync(string? modality = null, string? level = null)
    {
        var now = DateTime.UtcNow;

        var query = _db.ScheduledClasses
            .Include(s => s.ClassType)
            .Include(s => s.Instructor)
            .Include(s => s.Bookings)
            .Where(s => s.StartUtc > now && !s.IsCancelled);

        if (!string.IsNullOrEmpty(modality))
            query = query.Where(s => s.ClassType.Modalities.Contains(modality));

        if (!string.IsNullOrEmpty(level))
            query = query.Where(s => s.ClassType.Level.Contains(level));

        UpcomingClasses = await query
            .OrderBy(s => s.StartUtc)
            .Take(18)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostBookAsync(int scheduledClassId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
            return Challenge();

        var scheduledClass = await _db.ScheduledClasses
            .Include(s => s.Bookings)
            .Include(s => s.ClassType)
            .FirstOrDefaultAsync(s => s.Id == scheduledClassId);

        if (scheduledClass == null || scheduledClass.IsCancelled)
            return RedirectToPage(new { error = "Class not available" });

        // Capacity check
        int currentBookings = scheduledClass.Bookings.Count(b => b.Status == BookingStatus.Reserved || b.Status == BookingStatus.Attended);
        if (currentBookings >= scheduledClass.EffectiveCapacity)
            return RedirectToPage(new { error = "Class is full" });

        // Prevent double booking
        bool alreadyBooked = await _db.Bookings.AnyAsync(b =>
            b.UserId == userId &&
            b.ScheduledClassId == scheduledClassId &&
            (b.Status == BookingStatus.Reserved || b.Status == BookingStatus.Attended));

        if (alreadyBooked)
            return RedirectToPage(new { error = "Already booked" });

        // Credit check (simple version - use first available credit pack)
        var credit = await _db.UserCredits
            .Where(c => c.UserId == userId && c.CreditsRemaining > 0 && (c.ExpiresAt == null || c.ExpiresAt > DateTime.UtcNow))
            .OrderBy(c => c.ExpiresAt ?? DateTime.MaxValue)
            .FirstOrDefaultAsync();

        if (credit == null)
            return RedirectToPage(new { error = "No credits available. Please purchase a package." });

        // Create booking
        var booking = new Booking
        {
            UserId = userId,
            ScheduledClassId = scheduledClassId,
            Status = BookingStatus.Reserved,
            CreditsUsed = 1,
            BookedAtUtc = DateTime.UtcNow
        };

        credit.CreditsRemaining -= 1;

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        return RedirectToPage("/App/Dashboard", new { success = "Class booked successfully!" });
    }
}