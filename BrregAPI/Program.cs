using System.Text.Json;
using System.Text.Json.Serialization;
using BrregAPI.Handlers;
using BrregAPI.Modals;
using BrregAPI.Modals.Database;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

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
            o.Cookie.SameSite = SameSiteMode.None; // for test only, so we can publish api, but use client locally
            o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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
    options.Cookie.SameSite = SameSiteMode.None;
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    //For development only, lets us publish api, but use client locally
    app.UseCookiePolicy(
        new CookiePolicyOptions()
        {
            MinimumSameSitePolicy = SameSiteMode.None,
            Secure = CookieSecurePolicy.Always
        }
    );
}

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
