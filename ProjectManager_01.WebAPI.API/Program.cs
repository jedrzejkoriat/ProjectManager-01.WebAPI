using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using ProjectManager_01.Application.Configuration;
using ProjectManager_01.Hubs;
using ProjectManager_01.Infrastructure.Configuration;
using ProjectManager_01.WebAPI.Configuration;
using ProjectManager_01.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger
builder.Services.AddSwaggerConfiguration();

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

// Http Context
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
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
