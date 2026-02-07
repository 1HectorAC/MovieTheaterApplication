using MovieTheaterApplication.Repositories;
using MovieTheaterApplication.Repositories.Implementations;
using MovieTheaterApplication.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IShowingRepository, ShowingRepository>();
builder.Services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
builder.Services.AddSingleton<TestDb>();
builder.Services.AddDbContext<MovieTheaterDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("Db_CONNECTION_STRING")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
