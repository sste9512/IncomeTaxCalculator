using FastEndpoints;
using FluentValidation;
using IncomeTaxCalculator.Server.Features.FastEndpoints.Tax;

namespace IncomeTaxCalculator.Server.Features.FastEndpoints.Tax.Validators
{
    public sealed class GetTaxBracketsValidator : Validator<GetTaxBracketsRequest>
    {
        public GetTaxBracketsValidator()
        {
            RuleFor(x => x.FilingStatus)
                .NotEmpty()
                .WithMessage("Filing status is required")
                .Must(status => status == "Single" || status == "Married")
                .WithMessage("Filing status must be 'Single' or 'Married'");
        }
    }
}
