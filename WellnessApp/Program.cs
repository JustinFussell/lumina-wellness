var builder = WebApplication.CreateBuilder(args);

// ============================================
// Lumina - Wellness Studio Platform Services
// ============================================

// Add Razor Pages
builder.Services.AddRazorPages();

// EF Core + SQLite (easy local dev, swap to Postgres later)
builder.Services.AddDbContext<Lumina.Data.ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=lumina.db"));

// ASP.NET Identity with roles
builder.Services.AddDefaultIdentity<Lumina.Data.ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Demo-friendly
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>()
.AddEntityFrameworkStores<Lumina.Data.ApplicationDbContext>();

// SignalR for live capacity updates (future Phase 3)
builder.Services.AddSignalR();

// Add custom Lumina services (will expand)
builder.Services.AddScoped<Lumina.Services.ScheduleService>();
builder.Services.AddScoped<Lumina.Services.BookingService>();
builder.Services.AddScoped<Lumina.Data.DatabaseSeeder>();

var app = builder.Build();

// ============================================
// HTTP Pipeline
// ============================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map SignalR hub (for live updates)
app.MapHub<Lumina.Hubs.BookingHub>("/bookinghub");

app.MapRazorPages();

// Seed demo data on startup (Rondebosch Lumina studio)
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<Lumina.Services.DatabaseSeeder>();
    await seeder.SeedAsync();
}

app.Run();
