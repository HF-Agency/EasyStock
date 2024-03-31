using EasyStock.API.EntityFramework;
using EasyStock.API.Services;
using EasyStock.Library.Entities;
using EasyStock.Library.Entities.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Db Context
builder.Services.AddDbContext<EasyStockContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyStockDb")));

// Configure Identity Services
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EasyStockContext>()
    .AddDefaultTokenProviders();

// Configure Cookies Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true; // Makes the cookie inaccessible to JavaScript
    options.ExpireTimeSpan = TimeSpan.FromDays(5); // Sets the cookie expiration time
    options.LoginPath = "/Auth/Login"; // Specifies the path for the login page
    options.LogoutPath = "/Auth/Logout"; // Specifies the path for the logout action
    options.SlidingExpiration = true; // Resets the expiration time after half the time has passed
                                      // Recommended to use secure cookies in production
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None; // Adjust based on your cross-site requirements
});

var app = builder.Build();

// Seed the database with an admin user and roles
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var context = scope.ServiceProvider.GetRequiredService<EasyStockContext>();
    await ApplicationDbInitializer.SeedUsers(userManager, roleManager, context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
