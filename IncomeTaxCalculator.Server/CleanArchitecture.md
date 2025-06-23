# Clean Architecture Implementation

This document describes the Clean Architecture implementation for the Income Tax Calculator Server project.

## Architecture Overview

The project follows Clean Architecture principles with clear separation of concerns across four main layers:

### 1. Domain Layer (`Domain/`)
The core business logic layer that contains:

#### Entities (`Domain/Entities/`)
- `TaxBracket.cs` - Core tax bracket entity with validation
- `TaxCalculation.cs` - Tax calculation aggregate root
- `TaxBracketCalculation.cs` - Value object for bracket calculations

#### Value Objects (`Domain/ValueObjects/`)
- `FilingStatus.cs` - Strongly typed filing status with validation

#### Interfaces (`Domain/Interfaces/`)
- `ITaxCalculatorDomainService.cs` - Domain service contract

**Key Principles:**
- No dependencies on external frameworks
- Contains business rules and validation
- Immutable entities with proper constructors

### 2. Application Layer (`Application/`)
Orchestrates business logic and handles use cases:

#### Common (`Application/Common/`)
- `Interfaces/ITaxCalculatorService.cs` - Application service contract
- `Models/Result.cs` - Error handling wrapper

#### Tax Calculation Feature (`Application/TaxCalculation/`)
- `Commands/CalculateTaxCommand.cs` - Tax calculation use case
- `Queries/GetTaxBracketsQuery.cs` - Get tax brackets use case
- `Queries/GetFilingStatusesQuery.cs` - Get filing statuses use case
- `DTOs/` - Data transfer objects for API contracts

**Key Principles:**
- Uses MediatR for CQRS pattern
- Result pattern for error handling
- DTOs for data transfer
- Depends only on Domain layer

### 3. Infrastructure Layer (`Infrastructure/`)
Implements application interfaces and handles external concerns:

#### Services (`Infrastructure/Services/`)
- `TaxCalculatorService.cs` - Application service implementation
- `TaxCalculatorDomainService.cs` - Domain service implementation

**Key Principles:**
- Implements Application interfaces
- Contains data access and external service integrations
- Maps between Domain objects and DTOs

### 4. Presentation Layer (`Presentation/`)
Handles HTTP requests and responses:

#### Controllers (`Presentation/Controllers/`)
- `TaxController.cs` - Traditional MVC controller

#### Endpoints (`Presentation/Endpoints/`)
- `CalculateTaxEndpoint.cs` - FastEndpoints implementation

**Key Principles:**
- Thin layer that delegates to Application layer
- Uses Result pattern for consistent error handling
- Proper HTTP status codes and error responses

## Dependency Direction

```
Presentation Layer
       ↓
Application Layer
       ↓
Domain Layer
       ↑
Infrastructure Layer
```

- **Domain Layer**: No dependencies
- **Application Layer**: Depends only on Domain
- **Infrastructure Layer**: Implements Application contracts, depends on Domain
- **Presentation Layer**: Depends on Application layer

## Key Patterns Implemented

### 1. CQRS (Command Query Responsibility Segregation)
- Commands for write operations (`CalculateTaxCommand`)
- Queries for read operations (`GetTaxBracketsQuery`, `GetFilingStatusesQuery`)

### 2. Result Pattern
- Consistent error handling across the application
- No exceptions for business logic failures
- Clear success/failure states

### 3. Repository Pattern (Ready for Extension)
- Domain service interfaces allow for easy data source switching
- Currently in-memory, easily replaceable with database implementations

### 4. Dependency Inversion
- High-level modules don't depend on low-level modules
- Both depend on abstractions (interfaces)

## Migration from Original Structure

The clean architecture maintains compatibility with existing APIs while providing:

1. **Better Testability**: Clear layer separation enables easier unit testing
2. **Maintainability**: Business logic is isolated from frameworks
3. **Flexibility**: Easy to swap implementations (databases, external services)
4. **Scalability**: Clear boundaries support team development

## Existing Features Preserved

- FastEndpoints configuration
- MediatR pipeline behaviors
- Swagger documentation
- Validation
- Logging

## Next Steps for Full Migration

1. **Update Existing Endpoints**: Move remaining FastEndpoints to Presentation layer
2. **Add Validators**: Implement FluentValidation for DTOs
3. **Database Integration**: Replace in-memory data with Entity Framework
4. **Add Domain Events**: Implement domain event pattern if needed
5. **Clean Up Old Files**: Remove original Models/ and Services/ folders after verification 