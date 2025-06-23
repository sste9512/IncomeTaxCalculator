using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;

namespace IncomeTaxCalculator.Server.Application.Common.Interfaces;

public interface ITaxCalculatorService
{
    Task<TaxCalculationResponseDto> CalculateTaxAsync(
        TaxCalculationRequestDto request,
        CancellationToken cancellationToken = default);
        
    Task<IReadOnlyList<TaxBracketDto>> GetTaxBracketsAsync(
        string taxSystem = "UK",
        CancellationToken cancellationToken = default);
        
    Task<IReadOnlyList<string>> GetTaxSystemsAsync(CancellationToken cancellationToken = default);
} 