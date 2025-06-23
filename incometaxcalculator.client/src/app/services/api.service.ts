import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '../api-client';
import {
  TaxCalculationRequest,
  TaxCalculationResponse,
  TaxBracket
} from '../models/tax-calculation.models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly baseUrl: string;

  constructor(private http: HttpClient, @Inject(API_BASE_URL) baseUrl: string) {
    this.baseUrl = `${baseUrl}/api`;
  }

  calculateTax(request: TaxCalculationRequest): Observable<TaxCalculationResponse> {
    return this.http.post<TaxCalculationResponse>(`${this.baseUrl}/tax/calculate`, request);
  }

  getTaxBrackets(taxSystem: string = 'UK'): Observable<TaxBracket[]> {
    return this.http.get<TaxBracket[]>(`${this.baseUrl}/tax/brackets/${taxSystem}`);
  }

  getTaxSystems(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/tax/taxsystems`);
  }
}
