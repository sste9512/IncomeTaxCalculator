using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Application.Common.Models;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;
using MediatR;

namespace IncomeTaxCalculator.Server.Application.TaxCalculation.Commands;

public sealed record CalculateTaxCommand(
    decimal GrossAnnualSalary,
    string? TaxSystem = "UK") : IRequest<Result<TaxCalculationResponseDto>>;

public sealed class CalculateTaxCommandHandler : IRequestHandler<CalculateTaxCommand, Result<TaxCalculationResponseDto>>
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public CalculateTaxCommandHandler(ITaxCalculatorService taxCalculatorService)
    {
        _taxCalculatorService = taxCalculatorService;
    }

    public async Task<Result<TaxCalculationResponseDto>> Handle(
        CalculateTaxCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var requestDto = new TaxCalculationRequestDto(
                request.GrossAnnualSalary,
                request.TaxSystem ?? "UK");

            var result = await _taxCalculatorService.CalculateTaxAsync(requestDto, cancellationToken);
            return Result<TaxCalculationResponseDto>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<TaxCalculationResponseDto>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<TaxCalculationResponseDto>.Failure($"An error occurred while calculating tax: {ex.Message}");
        }
    }
} 