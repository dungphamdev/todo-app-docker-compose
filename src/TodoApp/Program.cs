using Microsoft.EntityFrameworkCore;
using TodoApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
    var testEnv = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT");
    if (!string.IsNullOrEmpty(connString) && !string.IsNullOrEmpty(testEnv))
    {
        //run by docker compose
        options.UseNpgsql(connString);
    }
    else
    {
        //local
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();