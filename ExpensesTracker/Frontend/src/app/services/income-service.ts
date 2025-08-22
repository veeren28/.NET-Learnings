import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExpensesDTO } from '../Pages/expenses/expenses';
import { IncomeDTO } from '../Pages/incomes/incomes';

@Injectable({
  providedIn: 'root',
})
export class IncomeService {
  constructor(private http: HttpClient) {}
  baseUrl = 'https://localhost:5000/api/Income';
  Get(filters: any = {}) {
    const params = new HttpParams({ fromObject: filters });
    return this.http.get<IncomeDTO[]>(`${this.baseUrl}`, { params });
  }
  Post(income: any): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}`, income, {
      responseType: 'text' as 'json',
    });
  }

  Delete(id: number): Observable<string> {
    return this.http.delete<string>(`${this.baseUrl}/${id}`, {
      responseType: 'text' as 'json',
    });
  }
  Edit(id: number, income: any): Observable<string> {
    return this.http.patch<string>(`${this.baseUrl}/${id}`, income, {
      responseType: 'text' as 'json',
    });
  }
}
