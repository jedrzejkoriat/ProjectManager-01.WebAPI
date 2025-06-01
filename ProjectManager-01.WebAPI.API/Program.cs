using System.Data;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Hubs;
using ProjectManager_01.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixedlimit", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 500;
    });
});

builder.Services.AddSignalR();

builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<TicketsHub>("/hubs/tickets");

app.Run();
