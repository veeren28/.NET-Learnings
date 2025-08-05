// app.ts
import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { SideBar } from './Components/side-bar/side-bar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SideBar],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'ExpenseTracker';
}
