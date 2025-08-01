import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Dashboard } from './Pages/dashboard/dashboard';
import { SideBar } from './Components/side-bar/side-bar';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Dashboard, SideBar, RouterModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected title = 'ExpenseTracker';
}
