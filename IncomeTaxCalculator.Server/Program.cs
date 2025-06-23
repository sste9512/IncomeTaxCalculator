using System.Reflection;
using MediatR;
using FastEndpoints;
using Microsoft.AspNetCore.Cors;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Domain.Interfaces;
using IncomeTaxCalculator.Server.Infrastructure.Services;
using IncomeTaxCalculator.Server.Infrastructure.Data;
using IncomeTaxCalculator.Server.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.AddControllers();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200",
                "http://localhost:57652",
                "https://localhost:57652")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Configure OpenAPI with FastEndpoints.Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerDocument(options => {
    options.DocumentSettings = settings => {
        settings.Title = "Income Tax Calculator API";
        settings.Description = "An API for calculating income tax";
        settings.Version = "v1";
    };
});

// Register MediatR
builder.Services.AddMediatR(cfg => {
   // cfg.RegisterServicesFromAssembly(typeof(CalculateTaxCommand).Assembly);
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    // Add pipeline behaviors from Features/Common/Behaviors
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(IncomeTaxCalculator.Server.Features.Common.Behaviors.LoggingBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(IncomeTaxCalculator.Server.Features.Common.Behaviors.ValidationBehavior<,>));
});

// Entity Framework Configuration
builder.Services.AddDbContext<TaxCalculatorDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=taxcalculator.db"));

// Clean Architecture DI Registration
// Infrastructure Services
builder.Services.AddScoped<ITaxCalculatorDomainService, TaxCalculatorDomainService>();
builder.Services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();

// Repositories
builder.Services.AddScoped<ITaxBracketRepository, TaxBracketRepository>();

var app = builder.Build();

// Initialize database with migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaxCalculatorDbContext>();
    try
    {
        await context.Database.MigrateAsync();
        app.Logger.LogInformation("Database migrations applied successfully");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while applying database migrations");
    }
}

// Configure middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable CORS - must be before endpoints but after basic middleware
app.UseCors("AllowAngularDev");

app.UseDefaultFiles();
app.MapStaticAssets();


app.UseFastEndpoints(c => {
    c.Endpoints.RoutePrefix = "api";
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen(); // FastEndpoints Swagger generation
}

app.UseAuthorization();

app.MapControllers();

// FastEndpoints replace the minimal API endpoints

app.MapFallbackToFile("/index.html");

app.Run();
