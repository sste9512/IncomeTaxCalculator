/* This file will be replaced by the generated API client */

import { Injectable, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class TaxClient {
    constructor(private http: HttpClient) {}

    // Placeholder methods that will be replaced by generated code
    calculate(request: any): Observable<any> {
        return this.http.post<any>('/api/Tax/calculate', request);
    }

    getBrackets(filingStatus: string): Observable<any> {
        return this.http.get<any>(`/api/Tax/brackets/${filingStatus}`);
    }

    getFilingStatuses(): Observable<string[]> {
        return this.http.get<string[]>('/api/Tax/filingstatuses');
    }
}
