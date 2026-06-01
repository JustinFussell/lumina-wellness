using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lumina.Data;

/// <summary>
/// Seeds realistic demo data for the Lumina Rondebosch studio.
/// This makes the app immediately impressive for owners and members.
/// </summary>
public class DatabaseSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseSeeder(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await _db.Database.MigrateAsync();

        // Roles
        await EnsureRoleAsync("Member");
        await EnsureRoleAsync("Instructor");
        await EnsureRoleAsync("Owner");

        // Packages
        if (!await _db.Packages.AnyAsync())
        {
            _db.Packages.AddRange(
                new Package { Name = "5-Class Pass", Credits = 5, PriceZAR = 950, ValidityDays = 60, SortOrder = 1 },
                new Package { Name = "10-Class Pass", Credits = 10, PriceZAR = 1750, ValidityDays = 90, SortOrder = 2 },
                new Package { Name = "Monthly Unlimited", Credits = 999, PriceZAR = 1850, ValidityDays = 30, SortOrder = 3 }
            );
            await _db.SaveChangesAsync();
        }

        // Instructors
        if (!await _db.Instructors.AnyAsync())
        {
            _db.Instructors.AddRange(
                new Instructor { Name = "Thandi Mokoena", Bio = "Founder & Lead Teacher. 15 years teaching Pilates & Yoga with a deep focus on rehabilitation and nervous system health.", Specialties = "Reformer, Restorative, Prenatal" },
                new Instructor { Name = "Liam van der Berg", Bio = "Passionate about functional movement and helping people move without pain.", Specialties = "Mat Pilates, Contemporary Flow" },
                new Instructor { Name = "Aisha Khan", Bio = "Specialist in adaptive and inclusive movement practices.", Specialties = "Yoga, Chair Yoga, Low-Sensory" }
            );
            await _db.SaveChangesAsync();
        }

        // Class Types (very rich accessibility data)
        if (!await _db.ClassTypes.AnyAsync())
        {
            _db.ClassTypes.AddRange(
                new ClassType
                {
                    Name = "Reformer Pilates",
                    Slug = "reformer-pilates",
                    Description = "Intelligent, precise work on the reformer. Build deep core strength and move with ease.",
                    DurationMinutes = 55,
                    DefaultCapacity = 10,
                    Level = "All Levels",
                    Modalities = "Pilates",
                    AccessFeatures = "Chair options available • Prenatal modifications • Low lighting option on request • Scent-free studio",
                    WhatToExpect = "A focused, supportive class with individual attention and clear cues.",
                    WhatToBring = "Grip socks (available to purchase) • Water bottle",
                    AccentColor = "#B87D5C"
                },
                new ClassType
                {
                    Name = "Restorative Yoga",
                    Slug = "restorative-yoga",
                    Description = "Supported, long-held poses using props. Deep rest for body and nervous system.",
                    DurationMinutes = 75,
                    DefaultCapacity = 14,
                    Level = "All Levels",
                    Modalities = "Yoga",
                    AccessFeatures = "Fully prop-supported • Chair option • Low sensory (no music) • Quiet entry/exit welcome",
                    WhatToExpect = "You will be guided into deeply restful positions and encouraged to let go.",
                    WhatToBring = "Nothing — we provide everything",
                    AccentColor = "#8B9A7D"
                }
            );
            await _db.SaveChangesAsync();
        }

        // Add some upcoming classes for the next 10 days (demo)
        if (!await _db.ScheduledClasses.AnyAsync())
        {
            var reformer = await _db.ClassTypes.FirstAsync(c => c.Slug == "reformer-pilates");
            var restorative = await _db.ClassTypes.FirstAsync(c => c.Slug == "restorative-yoga");
            var thandi = await _db.Instructors.FirstAsync(i => i.Name.Contains("Thandi"));
            var liam = await _db.Instructors.FirstAsync(i => i.Name.Contains("Liam"));
            var aisha = await _db.Instructors.FirstAsync(i => i.Name.Contains("Aisha"));

            var baseDate = DateTime.UtcNow.Date.AddDays(1);

            var classesToAdd = new List<ScheduledClass>();

            for (int i = 0; i < 10; i++)
            {
                var day = baseDate.AddDays(i);
                
                // Morning Reformer with Thandi
                classesToAdd.Add(new ScheduledClass
                {
                    ClassTypeId = reformer.Id,
                    InstructorId = thandi.Id,
                    StartUtc = day.AddHours(7).AddMinutes(30),
                    EndUtc = day.AddHours(8).AddMinutes(25),
                    Room = "Reformer Studio"
                });

                // Evening Restorative with Aisha (very accessible)
                if (i % 2 == 0)
                {
                    classesToAdd.Add(new ScheduledClass
                    {
                        ClassTypeId = restorative.Id,
                        InstructorId = aisha.Id,
                        StartUtc = day.AddHours(18),
                        EndUtc = day.AddHours(19).AddMinutes(15),
                        Room = "Main Studio"
                    });
                }
            }

            _db.ScheduledClasses.AddRange(classesToAdd);
            await _db.SaveChangesAsync();
        }
    }

    private async Task EnsureRoleAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole(roleName));
    }
}