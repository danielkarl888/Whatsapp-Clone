using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WAppBIU_Server.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WAppBIU_ServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WAppBIU_ServerContext") ?? throw new InvalidOperationException("Connection string 'WAppBIU_ServerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ranks}/{action=Index}/{id?}");

app.Run();
