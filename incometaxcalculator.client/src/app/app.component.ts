import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApiService } from './services/api.service';
import { TaxCalculationResponse } from './models/tax-calculation.models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent {
  taxForm: FormGroup;
  taxResult: TaxCalculationResponse | null = null;
  isLoading = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService
  ) {
    this.taxForm = this.fb.group({
      grossAnnualSalary: ['', [Validators.required, Validators.min(0)]]
    });
  }

  onCalculate(): void {
    if (this.taxForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      
      const request = {
        grossAnnualSalary: this.taxForm.value.grossAnnualSalary,
        taxSystem: 'UK'
      };

      this.apiService.calculateTax(request).subscribe({
        next: (result) => {
          this.taxResult = result;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error calculating tax:', error);
          this.errorMessage = 'Error calculating tax. Please try again.';
          this.isLoading = false;
        }
      });
    } else {
      this.errorMessage = 'Please enter a valid salary amount.';
    }
  }

  formatCurrency(amount: number): string {
    return new Intl.NumberFormat('en-GB', {
      style: 'currency',
      currency: 'GBP',
      minimumFractionDigits: 2
    }).format(amount);
  }

  title = 'UK Income Tax Calculator';
}
