using IncomeTaxCalculator.Server.Models;
using IncomeTaxCalculator.Server.Services;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.TaxCalculation.Commands
{
    public record CalculateTaxCommand(decimal AnnualIncome, string? FilingStatus, int DeductionsCount) : IRequest<TaxCalculationResponse>;

    public class CalculateTaxCommandHandler(TaxCalculatorService taxService)
        : IRequestHandler<CalculateTaxCommand, TaxCalculationResponse>
    {
        public async Task<TaxCalculationResponse> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
        {
            var taxRequest = new TaxCalculationRequest
            {
                AnnualIncome = request.AnnualIncome,
                FilingStatus = request.FilingStatus,
                DeductionsCount = request.DeductionsCount
            };

            var result = await taxService.CalculateTax(taxRequest);
            return result;
        }
    }
}
