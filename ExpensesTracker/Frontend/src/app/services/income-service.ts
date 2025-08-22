import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExpensesDTO } from '../Pages/expenses/expenses';

@Injectable({
  providedIn: 'root',
})
export class IncomeService {
  constructor(private http: HttpClient) {}
  baseUrl = 'https://localhost:5000/api/Income';
  Get(filters: any = {}) {
    const params = new HttpParams({ fromObject: filters });
    return this.http.get(`${this.baseUrl}`, { params });
  }
  Post(income: any) {
    return this.http.post(`${this.baseUrl}`, income);
  }
}
