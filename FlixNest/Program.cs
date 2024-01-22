using FlixNest.AppServices;
using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.IAppServices;
using FlixNest.Models;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<FlixNestDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlixNest"));
});
builder.Services.AddDbContext<FlixNestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlixNestContextConnection"));
});

builder.Services.AddDefaultIdentity<AccountUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FlixNestContext>();


builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
});

builder.Services.AddRazorPages();

//Dependency Injection
builder.Services.AddTransient<IActorService, ActorService>();
builder.Services.AddTransient<IDirectorService, DirectorService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IMovieGenreService, MovieGenreService>();
builder.Services.AddTransient<IMovieActorService, MovieActorService>();
builder.Services.AddTransient<IMovieDirectorService, MovieDirectorService>();
builder.Services.AddTransient<IEpisodeService, EpisodeService>();
builder.Services.AddTransient<IYearService, YearService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IFollowService, FollowService>();
builder.Services.AddTransient<IMovieCommentService, MovieCommentService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ILogMovieService, LogMovieService>();
builder.Services.AddTransient<ILogEpisodeService, LogEpisodeService>();

builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHangfireDashboard();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;
app.MapRazorPages();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "default",
    areaName: "Admin",
    pattern: "admin/{controller}/{action=Index}/{id?}",
    defaults: new { controller = "Home", action = "Index" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
