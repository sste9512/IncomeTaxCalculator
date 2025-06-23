import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  TaxCalculationRequest, 
  TaxCalculationResponse, 
  TaxBracket 
} from '../models/tax-calculation.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly baseUrl = '/api';

  constructor(private http: HttpClient) { }

  calculateTax(request: TaxCalculationRequest): Observable<TaxCalculationResponse> {
    return this.http.post<TaxCalculationResponse>(`${this.baseUrl}/Tax/calculate`, request);
  }

  getTaxBrackets(taxSystem: string = 'UK'): Observable<TaxBracket[]> {
    return this.http.get<TaxBracket[]>(`${this.baseUrl}/Tax/brackets/${taxSystem}`);
  }

  getTaxSystems(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/Tax/taxsystems`);
  }
}
