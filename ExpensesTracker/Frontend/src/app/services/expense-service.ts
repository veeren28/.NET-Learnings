import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Expenses, ExpensesDTO } from '../Pages/expenses/expenses';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExpenseService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:5000/api/Expense';
  GetExpenses(filters: any = {}) {
    const params = new HttpParams({ fromObject: filters });
    return this.http.get<ExpensesDTO[]>(`${this.baseUrl}`, { params });
  }
  PushExpenses(expense: any): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}`, expense, {
      responseType: 'text' as 'json',
    });
  }

  DeleteExpense(id: number): Observable<HttpResponse<String>> {
    return this.http.delete<string>(`${this.baseUrl}/${id}`, {
      responseType: 'text' as 'json',
      observe: 'response',
    });
  }

  EditExpense(id: number, expense: any): Observable<HttpResponse<String>> {
    return this.http.patch<string>(`${this.baseUrl}/${id}`, expense, {
      responseType: 'text' as 'json',
      observe: 'response',
    });
  }
}
