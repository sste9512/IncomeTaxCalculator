using MediatR;
using FastEndpoints;
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

// Configure OpenAPI with NSwag and FastEndpoints.Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => {
    config.Title = "Income Tax Calculator API";
    config.Description = "An API for calculating income tax";
    config.Version = "v1";
});
builder.Services.SwaggerDocument();

// Register MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
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

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
// app.UseFastEndpoints(c => {
//     c.Endpoints.RoutePrefix = "api";
// });

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(); // Serve OpenAPI/Swagger documents
    app.UseSwaggerUi(); // Serve Swagger UI
    app.UseSwaggerGen(); // FastEndpoints Swagger generation
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// FastEndpoints replace the minimal API endpoints

app.MapFallbackToFile("/index.html");

app.Run();
