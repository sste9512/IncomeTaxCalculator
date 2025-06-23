export interface TaxCalculationRequest {
  grossAnnualSalary: number;
  taxSystem?: string;
}

export interface TaxCalculationResponse {
  grossAnnualSalary: number;
  taxSystem: string;
  taxableIncome: number;
  annualTaxAmount: number;
  netAnnualSalary: number;
  monthlyGrossSalary: number;
  monthlyTaxAmount: number;
  monthlyNetSalary: number;
  effectiveTaxRate: number;
  bracketBreakdown: TaxBracketCalculation[];
}

export interface TaxBracketCalculation {
  bandName: string;
  lowerLimit: number;
  upperLimit?: number;
  ratePercentage: number;
  taxableAmount: number;
  taxAmount: number;
}

export interface TaxBracket {
  bandName: string;
  lowerLimit: number;
  upperLimit?: number;
  ratePercentage: number;
  salaryRange: string;
  description: string;
} 