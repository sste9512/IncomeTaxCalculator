using FastEndpoints;
using IncomeTaxCalculator.Server.Features.FastEndpoints.Tax;
using IncomeTaxCalculator.Server.Models;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax.Mapping
{
    public sealed class TaxMappings : Mapper<TaxCalculationRequest, TaxCalculationResponse, TaxCalculationRequest>
    {
        public override TaxCalculationRequest ToEntity(TaxCalculationRequest r) => new()
        {
            AnnualIncome = r.AnnualIncome,
            FilingStatus = r.FilingStatus,
            DeductionsCount = r.DeductionsCount
        };
    }
}
