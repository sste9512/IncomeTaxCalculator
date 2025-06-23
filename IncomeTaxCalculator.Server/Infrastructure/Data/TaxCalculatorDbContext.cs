using IncomeTaxCalculator.Server.Domain.Entities;
using IncomeTaxCalculator.Server.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Server.Infrastructure.Data;

public class TaxCalculatorDbContext : DbContext
{
    public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options) : base(options)
    {
    }

    public DbSet<TaxBracket> TaxBrackets => Set<TaxBracket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaxCalculatorDbContext).Assembly);
        
        // Seed data
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed UK tax brackets based on the specification
        var ukBrackets = new List<TaxBracket>
        {
            new("UK", "Tax Band A", 0, 5000, 0),           // £0 - £5,000 at 0%
            new("UK", "Tax Band B", 5000, 20000, 20),      // £5,000 - £20,000 at 20%
            new("UK", "Tax Band C", 20000, null, 40)       // £20,000+ at 40%
        };

        modelBuilder.Entity<TaxBracket>().HasData(ukBrackets);
    }
} 