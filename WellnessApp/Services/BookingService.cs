using Lumina.Data;

namespace Lumina.Services;

public class BookingService
{
    private readonly Data.ApplicationDbContext _db;

    public BookingService(Data.ApplicationDbContext db)
    {
        _db = db;
    }

    // Core booking logic will live here (capacity checks, credit handling, etc.)
}