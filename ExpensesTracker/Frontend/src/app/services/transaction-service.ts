import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TransactionDTO } from '../Pages/dashboard/dashboard';
import { Observable } from 'rxjs';
import { TransactionInterface } from '../Components/trasnaction-card/trasnsaction-card';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private baseUrl = 'https://localhost:5000/api/Transaction';
  constructor(private http: HttpClient) {}
  Get(filters: any = {}): Observable<TransactionDTO[]> {
    const params = new HttpParams({ fromObject: filters as any });
    return this.http.get<TransactionDTO[]>(this.baseUrl, { params });
  }
  GetSummary() {
    return this.http.get<TransactionDTO[]>(`${this.baseUrl}/summary`);
  }
}
