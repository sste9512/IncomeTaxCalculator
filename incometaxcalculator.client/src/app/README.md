# API Client Usage

## Overview

This application uses auto-generated TypeScript clients for communicating with the backend API. The clients are generated using NSwag based on the OpenAPI/Swagger documentation of the backend.

## Generated Files

- `src/api-client.ts` - Contains all generated TypeScript clients and models

## How to Use

### In Components

```typescript
import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html',
  styleUrls: ['./tax-calculator.component.css']
})
export class TaxCalculatorComponent {
  constructor(private apiService: ApiService) {}

  calculateTax(annualIncome: number, filingStatus: string, deductionsCount: number): void {
    const request = {
      annualIncome: annualIncome,
      filingStatus: filingStatus,
      deductionsCount: deductionsCount
    };

    this.apiService.taxClient.calculate(request).subscribe(result => {
      console.log('Tax calculation result:', result);
      // Handle the result
    });
  }
}
```

### Client Regeneration

The TypeScript client is automatically regenerated when the server project is built in Debug configuration. If you need to manually regenerate the client:

1. Build the server project
2. The client will be generated at `src/api-client.ts`

## Troubleshooting

If you encounter issues with the generated client:

1. Check that the API is correctly documented with appropriate attributes
2. Ensure the NSwag configuration in `nswag.json` is correct
3. Try manually running the NSwag command: `dotnet nswag run nswag.json`
