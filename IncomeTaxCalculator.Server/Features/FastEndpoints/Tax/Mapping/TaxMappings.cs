using FastEndpoints;
using IncomeTaxCalculator.Server.Features.FastEndpoints.Tax;
using IncomeTaxCalculator.Server.Models;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax.Mapping
{
    public class TaxMappings : Mapper<CalculateTaxRequest, TaxCalculationResponse, TaxCalculationRequest>
    {
        public override TaxCalculationRequest ToEntity(CalculateTaxRequest r) => new()
        {
            AnnualIncome = r.AnnualIncome,
            FilingStatus = r.FilingStatus,
            DeductionsCount = r.DeductionsCount
        };
    }
}
