using IncomeTaxCalculator.Server.Models;
using MediatR;

namespace IncomeTaxCalculator.Server.Features.TaxCalculation.Queries
{
    public sealed record GetTaxBracketsQuery(string FilingStatus) : IRequest<List<TaxBracket>>;

    public sealed class GetTaxBracketsQueryHandler : IRequestHandler<GetTaxBracketsQuery, List<TaxBracket>>
    {
        
        
        // UK Income Tax Brackets for 2023/24
        /* 
        Tax Band                   Annual Salary Range (£)            Tax Rate (%)
        Tax Band A                 0 - 5000                          0
        Tax Band B                 5000 - 20000                      20
        Tax Band C                 20000+                            40
        */

        private List<TaxBracket> brackets = new List<TaxBracket>()
        {
            new TaxBracket()
            {
                LowerLimit = 0, UpperLimit = 5000, Rate = 0
            },
            new TaxBracket()
            {
                LowerLimit = 5000, UpperLimit = 20000, Rate = .2m
            },
            new TaxBracket()
            {
                LowerLimit = 20000, UpperLimit = decimal.MaxValue, Rate = .4m
            },
        };


        public Task<List<TaxBracket>> Handle(GetTaxBracketsQuery request, CancellationToken cancellationToken)
        {
            // In a real app, this would come from a database or configuration
            var brackets = request.FilingStatus == "Single" 
                ? new List<TaxBracket>
                {
                    new() { LowerLimit = 0, UpperLimit = 11000, Rate = 0.10m },
                    new() { LowerLimit = 11001, UpperLimit = 44725, Rate = 0.12m },
                    new() { LowerLimit = 44726, UpperLimit = 95375, Rate = 0.22m },
                    new() { LowerLimit = 95376, UpperLimit = 182100, Rate = 0.24m },
                    new() { LowerLimit = 182101, UpperLimit = 231250, Rate = 0.32m },
                    new() { LowerLimit = 231251, UpperLimit = 578125, Rate = 0.35m },
                    new() { LowerLimit = 578126, UpperLimit = decimal.MaxValue, Rate = 0.37m }
                }
                : new List<TaxBracket>
                {
                    new() { LowerLimit = 0, UpperLimit = 22000, Rate = 0.10m },
                    new() { LowerLimit = 22001, UpperLimit = 89450, Rate = 0.12m },
                    new() { LowerLimit = 89451, UpperLimit = 190750, Rate = 0.22m },
                    new() { LowerLimit = 190751, UpperLimit = 364200, Rate = 0.24m },
                    new() { LowerLimit = 364201, UpperLimit = 462500, Rate = 0.32m },
                    new() { LowerLimit = 462501, UpperLimit = 693750, Rate = 0.35m },
                    new() { LowerLimit = 693751, UpperLimit = decimal.MaxValue, Rate = 0.37m }
                };

            return Task.FromResult(brackets);
        }
    }
}
