using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;
using IncomeTaxCalculator.Server.Domain.Entities;
using IncomeTaxCalculator.Server.Domain.Interfaces;
using IncomeTaxCalculator.Server.Domain.ValueObjects;

namespace IncomeTaxCalculator.Server.Infrastructure.Services;

public class TaxCalculatorService : ITaxCalculatorService
{
    private readonly ITaxCalculatorDomainService _domainService;

    public TaxCalculatorService(ITaxCalculatorDomainService domainService)
    {
        _domainService = domainService;
    }

    public async Task<TaxCalculationResponseDto> CalculateTaxAsync(
        TaxCalculationRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var taxSystem = request.TaxSystem ?? "UK";
        var system = TaxSystem.FromString(taxSystem);
        
        var calculation = await _domainService.CalculateTaxAsync(
            request.GrossAnnualSalary,
            system,
            cancellationToken);

        return new TaxCalculationResponseDto(
            calculation.GrossAnnualSalary,
            calculation.TaxSystem,
            calculation.TaxableIncome,
            calculation.AnnualTaxAmount,
            calculation.NetAnnualSalary,
            calculation.MonthlyGrossSalary,
            calculation.MonthlyTaxAmount,
            calculation.MonthlyNetSalary,
            calculation.EffectiveTaxRate,
            calculation.BracketBreakdown.Select(MapToDto).ToList());
    }

    public async Task<IReadOnlyList<TaxBracketDto>> GetTaxBracketsAsync(
        string taxSystem = "UK",
        CancellationToken cancellationToken = default)
    {
        var system = TaxSystem.FromString(taxSystem);
        var brackets = await _domainService.GetTaxBracketsAsync(system, cancellationToken);

        return brackets.Select(b => new TaxBracketDto(
            b.BandName,
            b.LowerLimit,
            b.UpperLimit,
            b.RatePercentage,
            FormatSalaryRange(b.LowerLimit, b.UpperLimit),
            $"{b.BandName}: {b.RatePercentage}% on income from £{b.LowerLimit:N0}" + 
            (b.UpperLimit.HasValue ? $" to £{b.UpperLimit:N0}" : "+")
        )).ToList();
    }

    public async Task<IReadOnlyList<string>> GetTaxSystemsAsync(CancellationToken cancellationToken = default)
    {
        return await _domainService.GetTaxSystemsAsync(cancellationToken);
    }

    private static string FormatSalaryRange(decimal lowerLimit, decimal? upperLimit)
    {
        return upperLimit.HasValue 
            ? $"£{lowerLimit:N0} - £{upperLimit:N0}"
            : $"£{lowerLimit:N0}+";
    }

    private static TaxBracketCalculationDto MapToDto(TaxBracketCalculation calculation) =>
        new(
            calculation.BandName,
            calculation.LowerLimit,
            calculation.UpperLimit,
            calculation.RatePercentage,
            calculation.TaxableAmount,
            calculation.TaxAmount);
} 