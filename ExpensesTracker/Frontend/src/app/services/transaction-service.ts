import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private baseUrl = 'http://localhost:5000/api/TransactionController';
  constructor(private http: HttpClient) {}
  Get() {
    return this.http.get(`${this.baseUrl}`);
  }
}
