using System.Data;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using ProjectManager_01.Application.Authorization;
using ProjectManager_01.Application.Configuration;
using ProjectManager_01.Hubs;
using ProjectManager_01.Infrastructure.Configuration;
using ProjectManager_01.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDbConnection>(sp =>
{
    return new SqlConnection(connectionString);
});

// Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

// FluentValidation setup
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("ProjectManager_01.Application"));

// API requests limit setup
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixedlimit", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 500;
    });
});

// Services and repositories
builder.Services.AddSignalR();
builder.Services.AddApplicationMapper();
builder.Services.AddDapperRepositories();
builder.Services.AddServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthorizationHandler, ProjectPermissionHandler>();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    AuthorizationPolicies.RegisterPolicies(options);
});

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

app.MapHub<TicketsHub>("/hubs/tickets");
app.MapControllers();

app.Run();
