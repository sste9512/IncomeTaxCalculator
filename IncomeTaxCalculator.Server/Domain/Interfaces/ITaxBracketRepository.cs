using IncomeTaxCalculator.Server.Domain.Entities;

namespace IncomeTaxCalculator.Server.Domain.Interfaces;

public interface ITaxBracketRepository
{
    Task<IReadOnlyList<TaxBracket>> GetByTaxSystemAsync(
        string taxSystem, 
        CancellationToken cancellationToken = default);
        
    Task<IReadOnlyList<TaxBracket>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task AddRangeAsync(
        IEnumerable<TaxBracket> taxBrackets, 
        CancellationToken cancellationToken = default);
        
    Task<bool> ExistsForTaxSystemAsync(
        string taxSystem, 
        CancellationToken cancellationToken = default);
} 