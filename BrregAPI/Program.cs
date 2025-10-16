using System.Text.Json;
using System.Text.Json.Serialization;
using BrregAPI.Handlers;
using BrregAPI.Modals;
using BrregAPI.Modals.Database;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

// Configure forwarded headers for proxy/gateway support
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor
        | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BrregContext>();

builder.Services
    .AddIdentity<User, IdentityRole>(o =>
    {
        o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        o.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<BrregContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        CookieAuthenticationDefaults.AuthenticationScheme,
        o =>
        {
            o.Cookie.Name = "BrregAuth";
            o.Cookie.SameSite = SameSiteMode.Lax; // Lax works for same-site (frontend and backend on same domain)
            o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use secure when request is HTTPS
            o.Cookie.HttpOnly = true;
            o.Cookie.IsEssential = true;
            o.ExpireTimeSpan = TimeSpan.FromDays(7);
            o.SlidingExpiration = true;
        }
    );

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(7);
});

builder.Services.Configure<IdentityOptions>(o =>
{
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireDigit = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireLowercase = true;
    o.Password.RequiredLength = 6;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "BrregAuth";
    options.Cookie.SameSite = SameSiteMode.Lax; // Match the authentication cookie settings
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Match the authentication cookie settings
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

HangfireHandler.SetupHangfire(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use forwarded headers from gateway/proxy (must be first)
app.UseForwardedHeaders();

// Cookie policy must be set before CORS and Authentication
app.UseCookiePolicy(
    new CookiePolicyOptions()
    {
        MinimumSameSitePolicy = SameSiteMode.Lax,
        Secure = CookieSecurePolicy.SameAsRequest
    }
);

// CORS configuration
app.UseCors(
    b =>
        b.SetIsOriginAllowed(
                origin =>
                    new Uri(origin).Host == "localhost"
                    || new Uri(origin).Host.Contains(".sindrema.com")
            )
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
);

// Do NOT use HTTPS redirection - gateway handles HTTPS
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard(
    "/hangfire",
    new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } }
);

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    //To auto migrate database on startup
    var context = scope.ServiceProvider.GetRequiredService<BrregContext>();
    context.Database.Migrate();
}

HangfireHandler.SetupTasks();

app.Run();
