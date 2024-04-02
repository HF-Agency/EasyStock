using EasyStock.API.EntityFramework;
using EasyStock.API.Services;
using EasyStock.Library.Entities.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
    options.User.RequireUniqueEmail = true;

    // Password settings.
    options.Password.RequireDigit = true; // Requires a number between 0-9 in the password.
    options.Password.RequireLowercase = true; // Requires a lowercase character in the password.
    options.Password.RequireNonAlphanumeric = true; // Requires a non-alphanumeric character in the password.
    options.Password.RequireUppercase = true; // Requires an uppercase character in the password.
    options.Password.RequiredLength = 6; // Sets the minimum length of the password.
    options.Password.RequiredUniqueChars = 1; // Requires a number of unique characters in the password.
})
    .AddEntityFrameworkStores<EasyStockContext>()
    .AddDefaultTokenProviders();

// Add JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
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
