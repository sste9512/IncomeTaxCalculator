using FastEndpoints;
using FluentValidation;
using IncomeTaxCalculator.Server.Features.FastEndpoints.Tax;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax.Validators
{
    public class CalculateTaxValidator : Validator<CalculateTaxRequest>
    {
        public CalculateTaxValidator()
        {
            RuleFor(x => x.AnnualIncome)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Annual income must be a positive number");

            RuleFor(x => x.FilingStatus)
                .NotEmpty()
                .WithMessage("Filing status is required")
                .Must(status => status == "Single" || status == "Married")
                .WithMessage("Filing status must be 'Single' or 'Married'");

            RuleFor(x => x.DeductionsCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deductions count must be a non-negative number");
        }
    }
}
