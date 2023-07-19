using PruebaTecnica_API.Controllers;

var builder = WebApplication.CreateBuilder(args);

const string allowedAddress = "http://localhost:4200";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.WithOrigins(allowedAddress).AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

app.UseHttpLogging();
// Configure the HTTP request pipeline.

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
