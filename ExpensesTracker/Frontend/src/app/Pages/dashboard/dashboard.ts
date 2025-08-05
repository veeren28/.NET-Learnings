import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { SideBar } from '../../Components/side-bar/side-bar';
@Component({
  selector: 'app-dashboard',
  imports: [TrasnsactionCard, SideBar],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  totalIncome = 4500;
  totalExpense = 5000;
}
