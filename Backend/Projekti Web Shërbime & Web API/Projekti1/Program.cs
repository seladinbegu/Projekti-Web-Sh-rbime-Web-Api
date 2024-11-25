using Microsoft.EntityFrameworkCore;
using Projekti1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();
builder.Configuration["ConnectionStrings:UshqimiDb"] = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING");


// Retrieve and log the connection string to see if it is being set correctly
var ushqimiConnectionString = Environment.GetEnvironmentVariable("DB_USHQIMI_CONNECTION_STRING_NEW");
var dietaConnectionString = Environment.GetEnvironmentVariable("DB_DIETA_CONNECTION_STRING_NEW");
var recetaConnectionString = Environment.GetEnvironmentVariable("DB_RECETA_CONNECTION_STRING_NEW");

Console.WriteLine($"Ushqimi Connection String: {ushqimiConnectionString}");
Console.WriteLine($"Dieta Connection String: {dietaConnectionString}");
Console.WriteLine($"Receta Connection String: {recetaConnectionString}");


// Register DbContext for each database using the environment variables
builder.Services.AddDbContext<UshqimiDbContext>(options =>
    options.UseSqlServer(ushqimiConnectionString));

builder.Services.AddDbContext<DietaDbContext>(options =>
   options.UseSqlServer(dietaConnectionString));

builder.Services.AddDbContext<RecetaDbContext>(options =>
    options.UseSqlServer(recetaConnectionString));

// Register other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
