# MediatR Implementation

## Overview

This project uses MediatR to implement the mediator pattern, which helps to reduce coupling between components by encapsulating how the components interact with each other.

## Components

### Commands

Commands represent actions that change state. They follow the naming convention `VerbNounCommand` (e.g., `CalculateTaxCommand`).

### Queries

Queries represent actions that retrieve data without changing state. They follow the naming convention `GetNounQuery` (e.g., `GetTaxBracketsQuery`).

### Handlers

Handlers process commands and queries. Each handler implements `IRequestHandler<TRequest, TResponse>`.

### Behaviors

Behaviors implement cross-cutting concerns like logging and validation. They implement `IPipelineBehavior<TRequest, TResponse>`.

## Recommended Practices

1. **Command/Query Separation**: Keep commands and queries separate.
2. **Single Responsibility**: Each handler should do one thing and do it well.
3. **Validation**: Use the validation behavior to validate commands and queries.
4. **Logging**: Use the logging behavior to log command and query execution.

## Example Usage

```csharp
// Controller
[HttpPost]
public async Task<IActionResult> CalculateTax(TaxCalculationRequest request)
{
    var command = new CalculateTaxCommand(request.AnnualIncome, request.FilingStatus, request.DeductionsCount);
    var result = await _mediator.Send(command);
    return Ok(result);
}
```

## Adding New Features

1. Create a command or query record that implements `IRequest<TResponse>`
2. Create a handler class that implements `IRequestHandler<TRequest, TResponse>`
3. Implement the `Handle` method in the handler
4. Register any dependencies needed by the handler

## Future Improvements

- Add FluentValidation for command and query validation
- Implement CQRS with separate read and write models
- Add caching for queries
