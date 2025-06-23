using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IncomeTaxCalculator.Server.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaxCalculatorDbContext>
{
    public TaxCalculatorDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaxCalculatorDbContext>();
        
        // Use SQLite with a default connection string for design-time
        optionsBuilder.UseSqlite("Data Source=IncomeTaxCalculator.db");
        
        return new TaxCalculatorDbContext(optionsBuilder.Options);
    }
} 