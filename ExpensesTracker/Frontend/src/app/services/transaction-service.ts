import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private baseUrl = 'https://localhost:5000/api/Transaction';
  constructor(private http: HttpClient) {}
  Get(filters: any = {}) {
    const params = new HttpParams({ fromObject: filters });
    return this.http.get(`${this.baseUrl}`, { params });
  }
  GetSummary() {
    return this.http.get(`${this.baseUrl}/summary`);
  }
}
