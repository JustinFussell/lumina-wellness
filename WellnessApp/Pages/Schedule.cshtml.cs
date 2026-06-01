using Lumina.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Pages;

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
        var now = DateTime.UtcNow;

        UpcomingClasses = await _db.ScheduledClasses
            .Include(s => s.ClassType)
            .Include(s => s.Instructor)
            .Include(s => s.Bookings)
            .Where(s => s.StartUtc > now && !s.IsCancelled)
            .OrderBy(s => s.StartUtc)
            .Take(18)
            .ToListAsync();
    }
}