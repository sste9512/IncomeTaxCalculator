namespace IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;

public record TaxCalculationRequestDto(
    decimal GrossAnnualSalary,
    string? TaxSystem = "UK"); 