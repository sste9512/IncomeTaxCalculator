namespace IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;

public record TaxBracketDto(
    string BandName,
    decimal LowerLimit,
    decimal? UpperLimit,
    int RatePercentage,
    string SalaryRange,
    string Description); 