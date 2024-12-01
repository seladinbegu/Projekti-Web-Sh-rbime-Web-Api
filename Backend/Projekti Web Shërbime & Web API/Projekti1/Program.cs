using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Projekti1.User;
using Projekti1.User.Data;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Projekti1.Data;
using Microsoft.EntityFrameworkCore;
using Projekti1.Schema;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();
builder.Configuration["ConnectionStrings:UshqimiDb"] = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING");

var ushqimiConnectionString = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING_NEW");
var dietaConnectionString = Environment.GetEnvironmentVariable("DB_DIETA_CONNECTION_STRING_NEW");
var recetaConnectionString = Environment.GetEnvironmentVariable("DB_RECETA_CONNECTION_STRING_NEW");
var userConnectionString = Environment.GetEnvironmentVariable("DB_USER_CONNECTION_STRING");

Console.WriteLine($"Ushqimi Connection String: {ushqimiConnectionString}");
Console.WriteLine($"Dieta Connection String: {dietaConnectionString}");
Console.WriteLine($"Receta Connection String: {recetaConnectionString}");
Console.WriteLine($"User Connection String: {userConnectionString}");

builder.Services.AddDbContext<UshqimiDbContext>(options =>
    options.UseSqlServer(ushqimiConnectionString));
builder.Services.AddDbContext<DietaDbContext>(options =>
   options.UseSqlServer(dietaConnectionString));
builder.Services.AddDbContext<RecetaDbContext>(options =>
    options.UseSqlServer(recetaConnectionString));
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(userConnectionString));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SwaggerIgnoreDataKrijimitFilter>();

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InpositionLibrary API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy =>
        policy.RequireRole("User"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigninKey"])),
        RoleClaimType = ClaimTypes.Role
    };
})
.AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure roles are created at the application startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Ensure "Admin" role exists
    var roleExist = await roleManager.RoleExistsAsync("Admin");
    if (!roleExist)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Ensure "User" role exists
    roleExist = await roleManager.RoleExistsAsync("User");
    if (!roleExist)
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    // Create or assign roles to specific users (seladin, denis, alban)
    var usernamesWithAdminRole = new[] { "seladin", "denis", "alban" };
    foreach (var username in usernamesWithAdminRole)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
