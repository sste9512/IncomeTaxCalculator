namespace IncomeTaxCalculator.Server.Domain.Entities;

public class TaxCalculation
{
    public decimal GrossAnnualSalary { get; init; }
    public string TaxSystem { get; init; }
    public decimal TaxableIncome { get; init; }
    public decimal AnnualTaxAmount { get; init; }
    public decimal NetAnnualSalary { get; init; }
    public decimal MonthlyGrossSalary { get; init; }
    public decimal MonthlyTaxAmount { get; init; }
    public decimal MonthlyNetSalary { get; init; }
    public decimal EffectiveTaxRate { get; init; }
    public IReadOnlyList<TaxBracketCalculation> BracketBreakdown { get; init; }

    public TaxCalculation(
        decimal grossAnnualSalary,
        string taxSystem,
        decimal taxableIncome,
        decimal annualTaxAmount,
        decimal effectiveTaxRate,
        IReadOnlyList<TaxBracketCalculation> bracketBreakdown)
    {
        if (grossAnnualSalary < 0) throw new ArgumentException("Gross annual salary cannot be negative", nameof(grossAnnualSalary));
        if (string.IsNullOrWhiteSpace(taxSystem)) throw new ArgumentException("Tax system is required", nameof(taxSystem));
        if (taxableIncome < 0) throw new ArgumentException("Taxable income cannot be negative", nameof(taxableIncome));
        if (annualTaxAmount < 0) throw new ArgumentException("Tax amount cannot be negative", nameof(annualTaxAmount));

        GrossAnnualSalary = grossAnnualSalary;
        TaxSystem = taxSystem;
        TaxableIncome = taxableIncome;
        AnnualTaxAmount = annualTaxAmount;
        NetAnnualSalary = grossAnnualSalary - annualTaxAmount;
        MonthlyGrossSalary = grossAnnualSalary / 12;
        MonthlyTaxAmount = annualTaxAmount / 12;
        MonthlyNetSalary = NetAnnualSalary / 12;
        EffectiveTaxRate = effectiveTaxRate;
        BracketBreakdown = bracketBreakdown;
    }
}

public class TaxBracketCalculation
{
    public string BandName { get; init; }
    public decimal LowerLimit { get; init; }
    public decimal? UpperLimit { get; init; }
    public int RatePercentage { get; init; }
    public decimal TaxableAmount { get; init; }
    public decimal TaxAmount { get; init; }

    public TaxBracketCalculation(
        string bandName,
        decimal lowerLimit,
        decimal? upperLimit,
        int ratePercentage,
        decimal taxableAmount,
        decimal taxAmount)
    {
        if (string.IsNullOrWhiteSpace(bandName)) throw new ArgumentException("Band name is required", nameof(bandName));
        if (lowerLimit < 0) throw new ArgumentException("Lower limit cannot be negative", nameof(lowerLimit));
        if (upperLimit.HasValue && upperLimit < 0) throw new ArgumentException("Upper limit cannot be negative", nameof(upperLimit));
        if (ratePercentage < 0 || ratePercentage > 100) throw new ArgumentException("Rate percentage must be between 0 and 100", nameof(ratePercentage));
        if (taxableAmount < 0) throw new ArgumentException("Taxable amount cannot be negative", nameof(taxableAmount));
        if (taxAmount < 0) throw new ArgumentException("Tax amount cannot be negative", nameof(taxAmount));

        BandName = bandName;
        LowerLimit = lowerLimit;
        UpperLimit = upperLimit;
        RatePercentage = ratePercentage;
        TaxableAmount = taxableAmount;
        TaxAmount = taxAmount;
    }

    // Calculated property for rate as decimal
    public decimal Rate => RatePercentage / 100.0m;
} 