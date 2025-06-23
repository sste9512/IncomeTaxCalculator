using MediatR;

namespace IncomeTaxCalculator.Server.Features.TaxCalculation.Queries
{
    public sealed record GetFilingStatusesQuery : IRequest<string[]>;

    public sealed class GetFilingStatusesQueryHandler : IRequestHandler<GetFilingStatusesQuery, string[]>
    {
        public Task<string[]> Handle(GetFilingStatusesQuery request, CancellationToken cancellationToken)
        {
            // In a real app, this might come from a database or configuration
            return Task.FromResult(new[] { "Single", "Married" });
        }
    }
}
