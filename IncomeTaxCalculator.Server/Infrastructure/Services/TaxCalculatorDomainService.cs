using IncomeTaxCalculator.Server.Domain.Entities;
using IncomeTaxCalculator.Server.Domain.Interfaces;
using IncomeTaxCalculator.Server.Domain.ValueObjects;

namespace IncomeTaxCalculator.Server.Infrastructure.Services;

public class TaxCalculatorDomainService : ITaxCalculatorDomainService
{
    private readonly ITaxBracketRepository _taxBracketRepository;

    public TaxCalculatorDomainService(ITaxBracketRepository taxBracketRepository)
    {
        _taxBracketRepository = taxBracketRepository;
    }

    public async Task<TaxCalculation> CalculateTaxAsync(
        decimal grossAnnualSalary,
        string taxSystem = "UK",
        CancellationToken cancellationToken = default)
    {
        // Validate tax system
        var system = TaxSystem.FromString(taxSystem);
        
        // For UK system, taxable income equals gross salary (no deductions in this simple model)
        decimal taxableIncome = grossAnnualSalary;

        // Get tax brackets for tax system
        var brackets = await _taxBracketRepository.GetByTaxSystemAsync(system.Value, cancellationToken);
        decimal totalTax = 0;
        var bracketBreakdown = new List<TaxBracketCalculation>();

        // Calculate tax for each bracket according to UK rules
        foreach (var bracket in brackets)
        {
            if (taxableIncome <= bracket.LowerLimit)
                continue;

            // Calculate the amount of income that falls within this bracket
            decimal upperLimit = bracket.UpperLimit ?? decimal.MaxValue;
            decimal taxableInThisBracket = Math.Min(taxableIncome, upperLimit) - bracket.LowerLimit;
            
            // Only process if there's income in this bracket
            if (taxableInThisBracket > 0)
            {
                decimal taxForThisBracket = taxableInThisBracket * bracket.Rate;
                totalTax += taxForThisBracket;

                bracketBreakdown.Add(new TaxBracketCalculation(
                    bracket.BandName,
                    bracket.LowerLimit,
                    bracket.UpperLimit,
                    bracket.RatePercentage,
                    taxableInThisBracket,
                    taxForThisBracket));
            }
        }

        // Calculate effective tax rate
        decimal effectiveTaxRate = grossAnnualSalary > 0 ? (totalTax / grossAnnualSalary) * 100 : 0;

        return new TaxCalculation(
            grossAnnualSalary,
            system,
            taxableIncome,
            totalTax,
            effectiveTaxRate,
            bracketBreakdown);
    }

    public async Task<IReadOnlyList<TaxBracket>> GetTaxBracketsAsync(
        string taxSystem = "UK",
        CancellationToken cancellationToken = default)
    {
        var system = TaxSystem.FromString(taxSystem);
        return await _taxBracketRepository.GetByTaxSystemAsync(system.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetTaxSystemsAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(TaxSystem.GetValidSystems());
    }
} 