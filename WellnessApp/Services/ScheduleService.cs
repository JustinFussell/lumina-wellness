using Lumina.Data;

namespace Lumina.Services;

public class ScheduleService
{
    private readonly Data.ApplicationDbContext _db;

    public ScheduleService(Data.ApplicationDbContext db)
    {
        _db = db;
    }

    // Future: rich query methods for Schedule page with filters
}