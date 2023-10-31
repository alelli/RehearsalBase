global using RehearsalBase.Data;
global using RehearsalBase.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("Cookies")
    .AddCookie(options => options.LoginPath = "/Account/SignIn");
builder.Services.AddAuthorization();



var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
