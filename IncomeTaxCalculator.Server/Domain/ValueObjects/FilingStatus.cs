namespace IncomeTaxCalculator.Server.Domain.ValueObjects;

public sealed record TaxSystem
{
    public static readonly TaxSystem UK = new("UK");
    
    private static readonly TaxSystem[] _validSystems = { UK };
    
    public string Value { get; init; }

    private TaxSystem(string value)
    {
        Value = value;
    }

    public static TaxSystem FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return UK; // Default to UK system

        var taxSystem = _validSystems.FirstOrDefault(ts => 
            string.Equals(ts.Value, value, StringComparison.OrdinalIgnoreCase));
            
        return taxSystem ?? UK; // Default to UK if not found
    }

    public static IReadOnlyList<string> GetValidSystems() => 
        _validSystems.Select(ts => ts.Value).ToList();

    public override string ToString() => Value;
    
    public static implicit operator string(TaxSystem taxSystem) => taxSystem.Value;
} 