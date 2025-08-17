import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { routes } from '../app.routes';
import { Route, Routes } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseURL = 'https://localhost:5000/api/auth';
  constructor(private http: HttpClient) {}
  Login(user: any) {
    return this.http.post(`${this.baseURL}/Login`, user);
  }
  Register(user: any) {
    return this.http.post(`${this.baseURL}/Register`, user);
  }
  Logout(): void {
    localStorage.removeItem('token');
  }
}
