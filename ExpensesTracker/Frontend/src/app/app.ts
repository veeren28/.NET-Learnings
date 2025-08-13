// app.ts
import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { SideBar } from './Components/side-bar/side-bar';
import { authGuard } from './auth-guard';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SideBar, CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'ExpenseTracker';
  constructor(private http: HttpClient) {}
}
