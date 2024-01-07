using FlixNest.Areas.Identity.Data;
using FlixNest.Data;
using FlixNest.Models;
using FlixNest.Repository.AccountRepository;
using FlixNest.Repository.ActorRepository;
using FlixNest.Repository.CountryRepository;
using FlixNest.Repository.DirectorRepository;
using FlixNest.Repository.EpisodeRepository;
using FlixNest.Repository.FollowRepository;
using FlixNest.Repository.GenreRepository;
using FlixNest.Repository.LogEpisodeRepository;
using FlixNest.Repository.LogMovieRepository;
using FlixNest.Repository.MovieActorRepository;
using FlixNest.Repository.MovieCommentRepository;
using FlixNest.Repository.MovieDirectorRepository;
using FlixNest.Repository.MovieGenreRepository;
using FlixNest.Repository.MovieRepository;
using FlixNest.Repository.YearRepository;
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
builder.Services.AddTransient<IActorRepository, ActorRepository>();
builder.Services.AddTransient<IDirectorRepository, DirectorRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieGenreRepository, MovieGenreRepository>();
builder.Services.AddTransient<IMovieActorRepository, MovieActorRepository>();
builder.Services.AddTransient<IMovieDirectorRepository, MovieDirectorRepository>();
builder.Services.AddTransient<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddTransient<IYearRepository, YearRepository>();
builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IFollowRepository, FollowRepository>();
builder.Services.AddTransient<IMovieCommentRepository, MovieCommentRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<ILogMovieRepository, LogMovieRepository>();
builder.Services.AddTransient<ILogEpisodeRepository, LogEpisodeRepository>();

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
