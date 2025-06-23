using IncomeTaxCalculator.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IncomeTaxCalculator.Server.Infrastructure.Data.Configurations;

public class TaxBracketConfiguration : IEntityTypeConfiguration<TaxBracket>
{
    public void Configure(EntityTypeBuilder<TaxBracket> builder)
    {
        builder.ToTable("TaxBrackets");
        
        builder.HasKey(x => new { x.TaxSystem, x.BandName });
        
        builder.Property(x => x.TaxSystem)
            .HasMaxLength(50)
            .IsRequired();
            
        builder.Property(x => x.BandName)
            .HasMaxLength(100)
            .IsRequired();
            
        builder.Property(x => x.LowerLimit)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
            
        builder.Property(x => x.UpperLimit)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false); // Nullable for highest band
            
        builder.Property(x => x.RatePercentage)
            .IsRequired();
            
        builder.HasIndex(x => x.TaxSystem)
            .HasDatabaseName("IX_TaxBrackets_TaxSystem");
            
        builder.HasIndex(x => new { x.TaxSystem, x.LowerLimit })
            .HasDatabaseName("IX_TaxBrackets_TaxSystem_LowerLimit");
    }
} 