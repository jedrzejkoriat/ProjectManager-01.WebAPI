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

// Logger
builder.Logging.AddConsole();

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

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<TicketsHub>("/hubs/tickets");
    app.MapHealthChecks("/health");
    app.MapControllers();

    logger.LogInformation("Application starting...");

    app.Run();

    logger.LogInformation("Application started successfully.");
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Application failed to start.");
    throw;
}