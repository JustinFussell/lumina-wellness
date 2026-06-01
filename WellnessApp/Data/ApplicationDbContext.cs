using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClassType> ClassTypes { get; set; } = null!;
    public DbSet<Instructor> Instructors { get; set; } = null!;
    public DbSet<ScheduledClass> ScheduledClasses { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Package> Packages { get; set; } = null!;
    public DbSet<UserCredit> UserCredits { get; set; } = null!;
    public DbSet<Waiver> Waivers { get; set; } = null!;
    public DbSet<MemberNote> MemberNotes { get; set; } = null!;
    public DbSet<Lead> Leads { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Booking unique constraint: one active booking per user per scheduled class
        builder.Entity<Booking>()
            .HasIndex(b => new { b.UserId, b.ScheduledClassId })
            .IsUnique()
            .HasFilter("Status IN ('Reserved', 'Attended')");

        // ScheduledClass indexes for fast schedule queries
        builder.Entity<ScheduledClass>()
            .HasIndex(s => s.StartUtc);

        builder.Entity<ScheduledClass>()
            .HasIndex(s => new { s.StartUtc, s.IsCancelled });
    }
}