/* This file will be replaced by the generated API client */

import { Injectable, InjectionToken, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class TaxClient {
    private baseUrl: string;

    constructor(private http: HttpClient, @Inject(API_BASE_URL) baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    // Placeholder methods that will be replaced by generated code
    calculate(request: any): Observable<any> {
        return this.http.post<any>(`${this.baseUrl}/api/Tax/calculate`, request);
    }

    getBrackets(filingStatus: string): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/api/Tax/brackets/${filingStatus}`);
    }

    getFilingStatuses(): Observable<string[]> {
        return this.http.get<string[]>(`${this.baseUrl}/api/Tax/filingstatuses`);
    }
}
