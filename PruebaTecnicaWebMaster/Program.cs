using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BD_ControlVentasContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"))

);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Home/Index";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("General", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("admin") || context.User.IsInRole("seller") || context.User.IsInRole("accountant")));

    options.AddPolicy("Sell", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("admin") || context.User.IsInRole("seller")));

    options.AddPolicy("Accounting", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("admin") || context.User.IsInRole("accountant")));

    options.AddPolicy("Admin", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("admin")));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
