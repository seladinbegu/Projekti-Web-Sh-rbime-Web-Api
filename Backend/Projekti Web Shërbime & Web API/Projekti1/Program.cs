using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projekti1.Data;
using Projekti1.M2MRelations.Data;
using Projekti1.User;
using Projekti1.User.Data;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();
builder.Configuration["ConnectionStrings:UshqimiDb"] = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING");




var ushqimiConnectionString = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING");
var dietaConnectionString = Environment.GetEnvironmentVariable("DB_DIETA_CONNECTION_STRING");
var recetaConnectionString = Environment.GetEnvironmentVariable("DB_RECETA_CONNECTION_STRING");
var userConnectionString = Environment.GetEnvironmentVariable("DB_USER_CONNECTION_STRING");
var dieta_ushqimiString = Environment.GetEnvironmentVariable("DB_USHQIMI_DIETA_CONNECTION_STRING");


Console.WriteLine("Ushqimi Connection String: " + ushqimiConnectionString);
Console.WriteLine("Dieta Connection String: " + dietaConnectionString);
Console.WriteLine("Receta Connection String: " + recetaConnectionString);
Console.WriteLine("User Connection String: " + userConnectionString);
Console.WriteLine("Dieta-Ushqimi Connection String: " + dieta_ushqimiString);
var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwRootPath))
{
    Directory.CreateDirectory(wwwRootPath);  // Create wwwroot folder if it doesn't exist
}


builder.Services.AddDbContext<UshqimiDbContext>(options =>
    options.UseSqlServer(ushqimiConnectionString));
builder.Services.AddDbContext<DietaDbContext>(options =>
    options.UseSqlServer(dietaConnectionString));
builder.Services.AddDbContext<RecetaDbContext>(options =>
    options.UseSqlServer(recetaConnectionString));
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(userConnectionString));
builder.Services.AddDbContext<Dieta_UshqimiDbContext>(options =>
    options.UseSqlServer(dieta_ushqimiString));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();  // This is where we register IHttpClientFactory
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InpositionLibrary API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
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

builder.Services.AddIdentity<User, IdentityRole>()
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
}).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Ensure roles and users with roles are initialized (like Admin, User)
    // Your existing role setup code...
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseStaticFiles(); // Enable serving static files from wwwroot
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
