using Lumina.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages.Admin;

[Authorize(Roles = "Owner")]
public class ScheduleModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public ScheduleModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public List<ScheduledClass> UpcomingClasses { get; set; } = new();

    public async Task OnGetAsync()
    {
        UpcomingClasses = await _db.ScheduledClasses
            .Include(s => s.ClassType)
            .Include(s => s.Instructor)
            .Include(s => s.Bookings)
            .Where(s => s.StartUtc > DateTime.UtcNow)
            .OrderBy(s => s.StartUtc)
            .Take(30)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        var cls = await _db.ScheduledClasses.FindAsync(id);
        if (cls != null)
        {
            cls.IsCancelled = true;
            await _db.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}