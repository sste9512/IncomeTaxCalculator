using FastEndpoints;
using IncomeTaxCalculator.Server.Application.TaxCalculation.Commands;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;
using MediatR;

namespace IncomeTaxCalculator.Server.Presentation.Endpoints;

public class CalculateTaxEndpoint(IMediator mediator) : Endpoint<TaxCalculationRequestDto, TaxCalculationResponseDto>
{
    public override void Configure()
    {
        Post("tax/calculate");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Calculate UK income tax based on gross annual salary";
            s.Description = "Calculates the UK income tax and provides a breakdown by tax bands with monthly amounts";
            s.ExampleRequest = new TaxCalculationRequestDto(75000, "UK");
            s.Response<TaxCalculationResponseDto>(200, "The tax calculation result with annual and monthly breakdowns");
            s.Response(400, "Invalid request parameters");
        });

        // Use the validator if available
    }

    public override async Task HandleAsync(TaxCalculationRequestDto req, CancellationToken ct)
    {
        try
        {
            var command = new CalculateTaxCommand(
                req.GrossAnnualSalary,
                req.TaxSystem);

            var result = await mediator.Send(command, ct);

            if (result.IsSuccess && result.Value is not null)
            {
                await SendAsync(result.Value, cancellation: ct);
            }
            else
            {
                ThrowError(result.Error ?? "Unknown error", statusCode: 400);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error calculating tax");
            ThrowError("An error occurred while calculating taxes", statusCode: 500);
        }
    }
} 