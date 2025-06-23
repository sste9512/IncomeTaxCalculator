namespace IncomeTaxCalculator.Server.Domain.Entities;

public class TaxBracket
{
    public string TaxSystem { get; init; } = string.Empty;
    public string BandName { get; init; } = string.Empty;
    public decimal LowerLimit { get; init; }
    public decimal? UpperLimit { get; init; } // Nullable for the highest band
    public int RatePercentage { get; init; } // Store as percentage (0, 20, 40)

    // Parameterless constructor for EF Core
    private TaxBracket() { }

    public TaxBracket(string taxSystem, string bandName, decimal lowerLimit, decimal? upperLimit, int ratePercentage)
    {
        if (string.IsNullOrWhiteSpace(taxSystem)) throw new ArgumentException("Tax system is required", nameof(taxSystem));
        if (string.IsNullOrWhiteSpace(bandName)) throw new ArgumentException("Band name is required", nameof(bandName));
        if (lowerLimit < 0) throw new ArgumentException("Lower limit cannot be negative", nameof(lowerLimit));
        if (upperLimit.HasValue && upperLimit < 0) throw new ArgumentException("Upper limit cannot be negative", nameof(upperLimit));
        if (ratePercentage < 0 || ratePercentage > 100) throw new ArgumentException("Rate percentage must be between 0 and 100", nameof(ratePercentage));
        if (upperLimit.HasValue && lowerLimit > upperLimit) throw new ArgumentException("Lower limit cannot be greater than upper limit");

        TaxSystem = taxSystem;
        BandName = bandName;
        LowerLimit = lowerLimit;
        UpperLimit = upperLimit;
        RatePercentage = ratePercentage;
    }

    // Calculated property for rate as decimal
    public decimal Rate => RatePercentage / 100.0m;

    // Convenience constructor for backward compatibility
    public TaxBracket(decimal lowerLimit, decimal upperLimit, decimal rate) 
        : this("UK", "Legacy", lowerLimit, upperLimit == decimal.MaxValue ? null : upperLimit, (int)(rate * 100))
    {
    }
} 