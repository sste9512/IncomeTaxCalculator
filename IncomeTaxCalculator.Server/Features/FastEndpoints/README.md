# FastEndpoints Implementation

## Overview

This project uses FastEndpoints to implement API endpoints. FastEndpoints is a developer-friendly alternative to Minimal APIs and traditional Controller pattern. It offers class-based organization of endpoints with a focus on REPR (Request-Endpoint-Response) pattern.

## Folder Structure

Endpoints are organized by feature/domain:

```
Features/
  FastEndpoints/
    Tax/
      CalculateTaxEndpoint.cs
      GetTaxBracketsEndpoint.cs
      GetFilingStatusesEndpoint.cs
```

## Endpoint Structure

Each endpoint class follows this structure:

1. Request model (if needed)
2. Endpoint class that inherits from `Endpoint<TRequest, TResponse>`
3. Configuration method to define route, HTTP verb, permissions, etc.
4. Handler method to process the request and return a response

## Integration with MediatR

Endpoints use MediatR to handle the business logic by sending commands and queries.

## Example

```csharp
public class CalculateTaxEndpoint : Endpoint<CalculateTaxRequest, TaxCalculationResponse>
{
    private readonly IMediator _mediator;

    public CalculateTaxEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("tax/calculate");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CalculateTaxRequest req, CancellationToken ct)
    {
        var command = new CalculateTaxCommand(req.AnnualIncome, req.FilingStatus, req.DeductionsCount);
        var result = await _mediator.Send(command, ct);
        await SendAsync(result, cancellation: ct);
    }
}
```

## Benefits

- Clean separation of concerns
- Easier testing with isolated endpoint classes
- Improved request validation
- Better organization of API endpoints
- Rich API documentation with Swagger integration
