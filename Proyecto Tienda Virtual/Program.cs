using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Proyecto_Tienda_Virtual.Models;
using Proyecto_Tienda_Virtual.ModelsFromDb;
using Proyecto_Tienda_Virtual.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register scaffolded context for the existing database
builder.Services.AddDbContext<TienDaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute (
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapRazorPages();
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
    pattern: "{controller=Productoes}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
