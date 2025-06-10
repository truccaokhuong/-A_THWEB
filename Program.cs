using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TH_WEB.Data;
using TH_WEB.Models;
using TH_WEB.Services;
using TH_WEB.Services.Authorization;
using TH_WEB.Areas.Attractions;
using TH_WEB.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services (required for OAuth state)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "__THWeb-Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

// Add data protection (helps with OAuth state protection)
builder.Services.AddDataProtection();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Add external authentication providers
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
        googleOptions.SaveTokens = true;
        
        // Fix callback path
        googleOptions.CallbackPath = "/signin-google";
        
        // Configure correlation cookie settings to fix OAuth state issues
        googleOptions.CorrelationCookie.Name = "__Google-Correlation";
        googleOptions.CorrelationCookie.HttpOnly = true;
        googleOptions.CorrelationCookie.SameSite = SameSiteMode.Lax;
        googleOptions.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        googleOptions.CorrelationCookie.IsEssential = true;
        
        // Add scopes for basic profile information
        googleOptions.Scope.Add("openid");
        googleOptions.Scope.Add("profile");
        googleOptions.Scope.Add("email");
        
        // Configure events to handle OAuth flow
        googleOptions.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            Console.WriteLine($"Redirecting to Google OAuth: {context.RedirectUri}");
            return Task.CompletedTask;
        };
        
        googleOptions.Events.OnAccessDenied = context =>
        {
            Console.WriteLine("Google OAuth access denied");
            context.Response.Redirect("/Identity/Account/LoginRegister?error=access_denied");
            context.HandleResponse();
            return Task.CompletedTask;
        };
        
        googleOptions.Events.OnRemoteFailure = context =>
        {
            Console.WriteLine($"Google OAuth remote failure: {context.Failure?.Message}");
            context.Response.Redirect("/Identity/Account/LoginRegister?error=oauth_failure");
            context.HandleResponse();
            return Task.CompletedTask;
        };
    });

// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/LoginRegister";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Error/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
    
    // Configure cookie settings for OAuth compatibility
    options.Cookie.Name = "__THWeb-Auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.IsEssential = true;
});

// Add Services
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ICarRentalService, CarRentalService>();
builder.Services.AddScoped<ITravelPackageService, TravelPackageService>();
builder.Services.AddScoped<IBookingService, TH_WEB.Services.BookingService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<TH_WEB.Areas.Attractions.Repositories.IAttractionsRepository, TH_WEB.Areas.Attractions.Repositories.AttractionsRepository>();

// Add Authorization Services
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserActivityService, UserActivityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Configure status code pages
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add session middleware (must be before routing)
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add user activity logging middleware - temporarily disabled due to foreign key issues
// app.UseUserActivityLogging();

// Map area routes (Identity, Attractions, etc.) - Fix routing for LoginRegister action
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Add specific route for Identity/Account/LoginRegister
app.MapControllerRoute(
    name: "identity_login",
    pattern: "Identity/Account/{action=LoginRegister}",
    defaults: new { area = "Identity", controller = "Account" });

// Map specific Identity routes for authentication
app.MapControllerRoute(
    name: "identity_routes",
    pattern: "Identity/{controller=Account}/{action=LoginRegister}/{id?}");

// Map Attractions area routes
app.MapAttractionsRoutes();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Check if seeddata argument is provided
if (args.Length > 0 && args[0].ToLower() == "seeddata")
{
    // Run seed data only
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            
            Console.WriteLine("Starting database seeding...");
            context.Database.Migrate(); // This will apply migrations
            DbInitializer.Initialize(context);
            
            // Seed authorization data
            Console.WriteLine("Seeding authorization data...");
            await AuthorizationSeeder.SeedAsync(context, roleManager, userManager);
            
            Console.WriteLine("Database seeding completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        }
    }
    return; // Exit without starting the web server
}

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        context.Database.Migrate(); // This will apply migrations
        DbInitializer.Initialize(context);
        
        // Seed authorization data
        await AuthorizationSeeder.SeedAsync(context, roleManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
