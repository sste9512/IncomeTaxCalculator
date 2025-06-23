using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Application.Common.Models;
using MediatR;

namespace IncomeTaxCalculator.Server.Application.TaxCalculation.Queries;

public record GetTaxSystemsQuery : IRequest<Result<IReadOnlyList<string>>>;

public class GetTaxSystemsQueryHandler : IRequestHandler<GetTaxSystemsQuery, Result<IReadOnlyList<string>>>
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public GetTaxSystemsQueryHandler(ITaxCalculatorService taxCalculatorService)
    {
        _taxCalculatorService = taxCalculatorService;
    }

    public async Task<Result<IReadOnlyList<string>>> Handle(
        GetTaxSystemsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _taxCalculatorService.GetTaxSystemsAsync(cancellationToken);
            return Result<IReadOnlyList<string>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<string>>.Failure($"An error occurred while getting tax systems: {ex.Message}");
        }
    }
} 