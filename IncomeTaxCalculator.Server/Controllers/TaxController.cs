using Microsoft.AspNetCore.Mvc;
using IncomeTaxCalculator.Server.Models;
using MediatR;
using IncomeTaxCalculator.Server.Features.TaxCalculation.Commands;
using IncomeTaxCalculator.Server.Features.TaxCalculation.Queries;

namespace IncomeTaxCalculator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController(IMediator mediator, ILogger<TaxController> logger)
        : ControllerBase
    {
        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateTax(TaxCalculationRequest request)
        {
            try
            {
                var command = new CalculateTaxCommand(
                    request.AnnualIncome, 
                    request.FilingStatus, 
                    request.DeductionsCount);

                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error calculating tax");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("brackets/{filingStatus}")]
        public async Task<IActionResult> GetTaxBrackets(string filingStatus)
        {
            // Basic validation
            if (string.IsNullOrEmpty(filingStatus) || (filingStatus != "Single" && filingStatus != "Married"))
            {
                return BadRequest("Invalid filing status. Must be 'Single' or 'Married'.");
            }

            var query = new GetTaxBracketsQuery(filingStatus);
            var brackets = await mediator.Send(query);
            return Ok(brackets);
        }

        [HttpGet("filingstatuses")]
        public async Task<IActionResult> GetFilingStatuses()
        {
            var query = new GetFilingStatusesQuery();
            var statuses = await mediator.Send(query);
            return Ok(statuses);
        }
    }
}
