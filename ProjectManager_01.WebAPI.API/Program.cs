using System.Data;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Configuration;
using ProjectManager_01.Hubs;
using ProjectManager_01.Infrastructure.Configuration;
using ProjectManager_01.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
        options.JsonSerializerOptions.WriteIndented = true;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// API requests limit setup
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixedlimit", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 500;
    });
});

builder.Services.AddHttpContextAccessor();

// Database connection
builder.Services.AddDatabaseConnection(builder.Configuration);

// Package services
builder.Services.AddSignalR();

builder.Services.AddApplicationMapper();
builder.Services.AddDtoValidation();

// Repositories
builder.Services.AddDapperRepositories();

// Services
builder.Services.AddServices();

// Authentication and Authorization
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddJwtGenerator();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use middleware for global exception handling
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<TicketsHub>("/hubs/tickets");
app.MapControllers();

app.Run();
