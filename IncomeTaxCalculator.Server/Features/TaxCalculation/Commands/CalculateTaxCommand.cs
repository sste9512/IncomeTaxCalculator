using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Application.Common.Models;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;
using IncomeTaxCalculator.Server.Infrastructure.Services;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.TaxCalculation.Commands
{
    public sealed record CalculateTaxCommand(decimal AnnualIncome, string? FilingStatus, int DeductionsCount) : IRequest<Result<TaxCalculationResponseDto>>;

    public sealed class CalculateTaxCommandHandler(ITaxCalculatorService taxService)
        : IRequestHandler<CalculateTaxCommand, Result<TaxCalculationResponseDto>>
    {
        public async Task<Result<TaxCalculationResponseDto>> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
        {
            var taxRequest = new TaxCalculationRequestDto(
                request.AnnualIncome,
                request.FilingStatus);
            
            var result = await taxService.CalculateTaxAsync(taxRequest, cancellationToken);

            //var result = await taxService.CalculateTax(taxRequest);
            return Result<TaxCalculationResponseDto>.Success(result);
        }
    }
}