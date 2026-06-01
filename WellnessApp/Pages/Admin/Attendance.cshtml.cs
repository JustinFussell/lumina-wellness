using Lumina.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages.Admin;

[Authorize(Roles = "Owner")]
public class AttendanceModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public AttendanceModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public ScheduledClass? ScheduledClass { get; set; }
    public List<Booking> Bookings { get; set; } = new();

    public async Task OnGetAsync(int id)
    {
        ScheduledClass = await _db.ScheduledClasses
            .Include(s => s.ClassType)
            .Include(s => s.Instructor)
            .FirstOrDefaultAsync(s => s.Id == id);

        Bookings = await _db.Bookings
            .Include(b => b.User)
            .Where(b => b.ScheduledClassId == id && b.Status != BookingStatus.Cancelled)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(int id, int[] attendedIds)
    {
        var bookings = await _db.Bookings
            .Where(b => b.ScheduledClassId == id)
            .ToListAsync();

        foreach (var booking in bookings)
        {
            booking.Status = attendedIds.Contains(booking.Id) 
                ? BookingStatus.Attended 
                : BookingStatus.NoShow;
        }

        await _db.SaveChangesAsync();
        return RedirectToPage("/Admin/Index");
    }
}