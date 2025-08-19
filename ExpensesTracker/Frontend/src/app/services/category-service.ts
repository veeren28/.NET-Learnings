import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}
  private baseUrl = 'https://localhost:5000/api/Category';
  GetCategories() {
    return this.http.get(`${this.baseUrl}`);
  }
}
