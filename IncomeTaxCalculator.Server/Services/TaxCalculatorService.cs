using IncomeTaxCalculator.Server.Models;

namespace IncomeTaxCalculator.Server.Services
{
    public class TaxCalculatorService
    {
        private readonly Dictionary<string, List<TaxBracket>> _taxBrackets;

        public TaxCalculatorService()
        {
            // Initialize with sample tax brackets (these should be configured properly in a real app)
            _taxBrackets = new Dictionary<string, List<TaxBracket>>
            {
                ["Single"] =
                [
                    new() { LowerLimit = 0, UpperLimit = 11000, Rate = 0.10m },
                    new() { LowerLimit = 11001, UpperLimit = 44725, Rate = 0.12m },
                    new() { LowerLimit = 44726, UpperLimit = 95375, Rate = 0.22m },
                    new() { LowerLimit = 95376, UpperLimit = 182100, Rate = 0.24m },
                    new() { LowerLimit = 182101, UpperLimit = 231250, Rate = 0.32m },
                    new() { LowerLimit = 231251, UpperLimit = 578125, Rate = 0.35m },
                    new() { LowerLimit = 578126, UpperLimit = decimal.MaxValue, Rate = 0.37m }
                ],
                ["Married"] =
                [
                    new() { LowerLimit = 0, UpperLimit = 22000, Rate = 0.10m },
                    new() { LowerLimit = 22001, UpperLimit = 89450, Rate = 0.12m },
                    new() { LowerLimit = 89451, UpperLimit = 190750, Rate = 0.22m },
                    new() { LowerLimit = 190751, UpperLimit = 364200, Rate = 0.24m },
                    new() { LowerLimit = 364201, UpperLimit = 462500, Rate = 0.32m },
                    new() { LowerLimit = 462501, UpperLimit = 693750, Rate = 0.35m },
                    new() { LowerLimit = 693751, UpperLimit = decimal.MaxValue, Rate = 0.37m }
                ]
            };
        }

        public async Task<TaxCalculationResponse> CalculateTax(TaxCalculationRequest request)
        {
            // Validate filing status
            if (string.IsNullOrEmpty(request.FilingStatus) || !_taxBrackets.ContainsKey(request.FilingStatus))
            {
                throw new ArgumentException("Invalid filing status", nameof(request.FilingStatus));
            }

            // Calculate standard deduction (simplified example)
            decimal standardDeduction = request.FilingStatus == "Single" ? 12950 : 25900;
            decimal additionalDeduction = request.DeductionsCount * 1000; // Example of additional deductions

            // Calculate taxable income
            decimal taxableIncome = Math.Max(0, request.AnnualIncome - standardDeduction - additionalDeduction);

            // Get tax brackets for filing status
            var brackets = _taxBrackets[request.FilingStatus];
            decimal totalTax = 0;
            var bracketBreakdown = new List<TaxBracketCalculation>();

            // Calculate tax for each bracket
            foreach (var bracket in brackets)
            {
                if (taxableIncome <= bracket.LowerLimit)
                    continue;

                decimal upperLimit = bracket.UpperLimit;
                decimal taxableInBracket = Math.Min(taxableIncome, upperLimit) - bracket.LowerLimit + 1;
                decimal taxForBracket = taxableInBracket * bracket.Rate;

                totalTax += taxForBracket;

                bracketBreakdown.Add(new TaxBracketCalculation
                {
                    LowerLimit = bracket.LowerLimit,
                    UpperLimit = bracket.UpperLimit,
                    Rate = bracket.Rate,
                    TaxableAmount = taxableInBracket,
                    Amount = taxForBracket
                });

                if (taxableIncome <= upperLimit)
                    break;
            }

            // Calculate effective tax rate
            decimal effectiveTaxRate = request.AnnualIncome > 0 ? (totalTax / request.AnnualIncome) * 100 : 0;

            return new TaxCalculationResponse
            {
                TaxableIncome = taxableIncome,
                TaxAmount = totalTax,
                EffectiveTaxRate = effectiveTaxRate,
                BracketBreakdown = bracketBreakdown
            };
        }
    }
}
