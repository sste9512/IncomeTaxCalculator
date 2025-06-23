using FastEndpoints;
using IncomeTaxCalculator.Server.Features.TaxCalculation.Queries;
using IncomeTaxCalculator.Server.Models;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax
{
    public sealed record GetTaxBracketsRequest
    {
        public string FilingStatus { get; set; } = string.Empty;
    }

    public sealed class GetTaxBracketsEndpoint(IMediator mediator) : Endpoint<GetTaxBracketsRequest, List<TaxBracket>>
    {
        public override void Configure()
        {
            Get("tax/brackets/{FilingStatus}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get tax brackets for a specific filing status";
                s.Description = "Returns the tax brackets with their rates for the given filing status";
                s.Response<List<TaxBracket>>(200, "The tax brackets");
                s.Response(400, "Invalid filing status");
            });

            // Use the validator if available
        }

        public override async Task HandleAsync(GetTaxBracketsRequest req, CancellationToken ct)
        {
            var query = new GetTaxBracketsQuery(req.FilingStatus);
            var brackets = await mediator.Send(query, ct);
            await SendAsync(brackets, cancellation: ct);
        }
    }
}
