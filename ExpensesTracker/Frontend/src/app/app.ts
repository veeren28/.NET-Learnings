import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Dashboard } from './Pages/dashboard/dashboard';
import { SideBar } from './Components/side-bar/side-bar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Dashboard, SideBar],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'ExpenseTracker';
}
