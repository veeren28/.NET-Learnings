import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:5000/api/Expense';
  GetExpenses(filters: any = {}) {
    const params = new HttpParams({ fromObject: filters });
    return this.http.get(`${this.baseUrl}`, { params });
  }
}
