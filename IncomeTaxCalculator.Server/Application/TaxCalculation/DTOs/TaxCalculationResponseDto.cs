namespace IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;

public record TaxCalculationResponseDto(
    decimal GrossAnnualSalary,
    string TaxSystem,
    decimal TaxableIncome,
    decimal AnnualTaxAmount,
    decimal NetAnnualSalary,
    decimal MonthlyGrossSalary,
    decimal MonthlyTaxAmount,
    decimal MonthlyNetSalary,
    decimal EffectiveTaxRate,
    IReadOnlyList<TaxBracketCalculationDto> BracketBreakdown);

public record TaxBracketCalculationDto(
    string BandName,
    decimal LowerLimit,
    decimal? UpperLimit,
    int RatePercentage,
    decimal TaxableAmount,
    decimal TaxAmount); 