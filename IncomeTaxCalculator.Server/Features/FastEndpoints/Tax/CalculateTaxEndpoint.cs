using FastEndpoints;
using IncomeTaxCalculator.Server.Features.TaxCalculation.Commands;
using IncomeTaxCalculator.Server.Models;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax
{
    public class CalculateTaxRequest
    {
        public decimal AnnualIncome { get; set; }
        public string? FilingStatus { get; set; }
        public int DeductionsCount { get; set; }
    }

    public class CalculateTaxEndpoint(IMediator mediator) : Endpoint<CalculateTaxRequest, TaxCalculationResponse>
    {
        public override void Configure()
        {
            Post("tax/calculate");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Calculate income tax based on annual income, filing status, and deductions";
                s.Description = "Calculates the income tax and provides a breakdown by tax brackets";
                s.ExampleRequest = new CalculateTaxRequest
                {
                    AnnualIncome = 75000,
                    FilingStatus = "Single",
                    DeductionsCount = 1
                };
                s.Response<TaxCalculationResponse>(200, "The tax calculation result");
                s.Response(400, "Invalid request parameters");
            });

            // Use the validator if available
        }

        public override async Task HandleAsync(CalculateTaxRequest req, CancellationToken ct)
        {
            try
            {
                var command = new CalculateTaxCommand(
                    req.AnnualIncome,
                    req.FilingStatus,
                    req.DeductionsCount);

                var result = await mediator.Send(command, ct);
                await SendAsync(result, cancellation: ct); 
            }
            catch (ArgumentException ex)
            {
                ThrowError(ex.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error calculating tax");
                ThrowError("An error occurred while calculating taxes", statusCode: 500);
            }
        }
    }
}
