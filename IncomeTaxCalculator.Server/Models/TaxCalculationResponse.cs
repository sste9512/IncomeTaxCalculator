namespace IncomeTaxCalculator.Server.Models
{
    public class TaxCalculationResponse
    {
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal EffectiveTaxRate { get; set; }
        public List<TaxBracketCalculation> BracketBreakdown { get; set; } = new();
    }

    public class TaxBracketCalculation
    {
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxableAmount { get; set; }
    }
}
