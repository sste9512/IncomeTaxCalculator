namespace IncomeTaxCalculator.Server.Models
{
    public class TaxCalculationRequest
    {
        public decimal AnnualIncome { get; set; }
        public string? FilingStatus { get; set; }
        public int DeductionsCount { get; set; }
    }
}
