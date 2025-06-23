using IncomeTaxCalculator.Server.Domain.Entities;
using IncomeTaxCalculator.Server.Domain.Interfaces;
using IncomeTaxCalculator.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Server.Infrastructure.Data.Repositories;

public class TaxBracketRepository : ITaxBracketRepository
{
    private readonly TaxCalculatorDbContext _context;

    public TaxBracketRepository(TaxCalculatorDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaxBracket>> GetByTaxSystemAsync(
        string taxSystem, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TaxBrackets
                .Where(tb => tb.TaxSystem == taxSystem)
                // .OrderBy(tb => tb.LowerLimit)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error retrieving tax brackets for tax system '{taxSystem}'", ex);
        }
    }

    public async Task<IReadOnlyList<TaxBracket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TaxBrackets
                .OrderBy(tb => tb.TaxSystem)
                .ThenBy(tb => tb.LowerLimit)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error retrieving all tax brackets", ex);
        }
    }

    public async Task AddRangeAsync(
        IEnumerable<TaxBracket> taxBrackets, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.TaxBrackets.AddRangeAsync(taxBrackets, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error adding tax brackets", ex);
        }
    }

    public async Task<bool> ExistsForTaxSystemAsync(
        string taxSystem, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TaxBrackets
                .AnyAsync(tb => tb.TaxSystem == taxSystem, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error checking if tax brackets exist for tax system '{taxSystem}'", ex);
        }
    }
} 