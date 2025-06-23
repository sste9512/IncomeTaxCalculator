namespace IncomeTaxCalculator.Server.Models
{
    public class TaxBracket
    {
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal Rate { get; set; }
    }
}
