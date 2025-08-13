import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { SideBar } from '../../Components/side-bar/side-bar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../services/transaction-service';
@Component({
  selector: 'app-dashboard',
  imports: [TrasnsactionCard, SideBar, CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  totalIncome = 4500;
  totalExpense = 5000;
  constructor(private serv: TransactionService) {}
  ngOnInit() {
    var transactions: any;
    transactions = this.serv.Get();
    console.log(transactions);
  }
}
