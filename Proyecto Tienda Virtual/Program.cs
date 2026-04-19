using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Proyecto_Tienda_Virtual.Models;
using Proyecto_Tienda_Virtual.ModelsFromDb;
using Proyecto_Tienda_Virtual.Data;

var builder = WebApplication.CreateBuilder(args);

// Se le agrega servicios al contenedor.
builder.Services.AddSession();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Se registra el contexto estructurado para la bade de datos existente.
builder.Services.AddDbContext<TienDaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute (
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapRazorPages();
// Configurar la canalizacion de solicitudes de HTTP..
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.Run();
