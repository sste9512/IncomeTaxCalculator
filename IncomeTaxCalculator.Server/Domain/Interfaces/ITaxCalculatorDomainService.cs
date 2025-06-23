using IncomeTaxCalculator.Server.Domain.Entities;

namespace IncomeTaxCalculator.Server.Domain.Interfaces;

public interface ITaxCalculatorDomainService
{
    Task<TaxCalculation> CalculateTaxAsync(
        decimal grossAnnualSalary,
        string taxSystem = "UK",
        CancellationToken cancellationToken = default);
        
    Task<IReadOnlyList<TaxBracket>> GetTaxBracketsAsync(
        string taxSystem = "UK",
        CancellationToken cancellationToken = default);
        
    Task<IReadOnlyList<string>> GetTaxSystemsAsync(CancellationToken cancellationToken = default);
} 