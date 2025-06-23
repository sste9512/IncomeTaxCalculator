using IncomeTaxCalculator.Server.Application.Common.Interfaces;
using IncomeTaxCalculator.Server.Application.Common.Models;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;
using MediatR;

namespace IncomeTaxCalculator.Server.Application.TaxCalculation.Queries;

public record GetTaxBracketsQuery(string TaxSystem = "UK") : IRequest<Result<IReadOnlyList<TaxBracketDto>>>;

public class GetTaxBracketsQueryHandler : IRequestHandler<GetTaxBracketsQuery, Result<IReadOnlyList<TaxBracketDto>>>
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public GetTaxBracketsQueryHandler(ITaxCalculatorService taxCalculatorService)
    {
        _taxCalculatorService = taxCalculatorService;
    }

    public async Task<Result<IReadOnlyList<TaxBracketDto>>> Handle(
        GetTaxBracketsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _taxCalculatorService.GetTaxBracketsAsync(request.TaxSystem, cancellationToken);
            return Result<IReadOnlyList<TaxBracketDto>>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<IReadOnlyList<TaxBracketDto>>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<IReadOnlyList<TaxBracketDto>>.Failure($"An error occurred while getting tax brackets: {ex.Message}");
        }
    }
} 