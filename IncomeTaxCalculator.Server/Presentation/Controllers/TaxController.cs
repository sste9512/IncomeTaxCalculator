using Microsoft.AspNetCore.Mvc;
using MediatR;
using IncomeTaxCalculator.Server.Application.TaxCalculation.Commands;
using IncomeTaxCalculator.Server.Application.TaxCalculation.Queries;
using IncomeTaxCalculator.Server.Application.TaxCalculation.DTOs;

namespace IncomeTaxCalculator.Server.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaxController> _logger;

    public TaxController(IMediator mediator, ILogger<TaxController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateTax([FromBody] TaxCalculationRequestDto request)
    {
        try
        {
            var command = new CalculateTaxCommand(
                request.GrossAnnualSalary,
                request.TaxSystem);

            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(
                onSuccess: value => Ok(value),
                onFailure: error => BadRequest(error));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating tax");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("brackets/{taxSystem?}")]
    public async Task<IActionResult> GetTaxBrackets(string taxSystem = "UK")
    {
        try
        {
            var query = new GetTaxBracketsQuery(taxSystem);
            var result = await _mediator.Send(query);

            return result.Match<IActionResult>(
                onSuccess: value => Ok(value),
                onFailure: error => BadRequest(error));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tax brackets");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("taxsystems")]
    public async Task<IActionResult> GetTaxSystems()
    {
        try
        {
            var query = new GetTaxSystemsQuery();
            var result = await _mediator.Send(query);

            return result.Match<IActionResult>(
                onSuccess: value => Ok(value),
                onFailure: error => BadRequest(error));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tax systems");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
} 