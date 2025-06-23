using FastEndpoints;
using IncomeTaxCalculator.Server.Features.TaxCalculation.Queries;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax
{
    public sealed class GetFilingStatusesEndpoint : EndpointWithoutRequest<string[]>
    {
        private readonly IMediator _mediator;

        public GetFilingStatusesEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("tax/filingstatuses");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Get all available filing statuses";
                s.Description = "Returns the list of all supported filing statuses";
                s.Response<string[]>(200, "The list of filing statuses");
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new GetFilingStatusesQuery();
            var statuses = await _mediator.Send(query, ct);
            await SendAsync(statuses, cancellation: ct);
        }
    }
}
