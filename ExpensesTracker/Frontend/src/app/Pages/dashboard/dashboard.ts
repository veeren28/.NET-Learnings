import { Component } from '@angular/core';
import {
  TransactionInterface,
  TrasnsactionCard,
} from '../../Components/trasnaction-card/trasnsaction-card';
import { SideBar } from '../../Components/side-bar/side-bar';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../../services/transaction-service';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-dashboard',
  imports: [TrasnsactionCard, SideBar, CommonModule, FormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  constructor(private serv: TransactionService) {}
  filters = {
    categoryName: '',
    startdate: '',
    endDate: '',
    title: '', //used for search
    minAmount: '',
    maxAmount: '',
    search: '',
  };
  ngOnInit() {
    this.loadTransactions();
    this.GetSummary();
  }
  transactions!: TransactionInterface[];
  loadTransactions() {
    this.serv.Get(this.filters).subscribe((data: TransactionDTO[]) => {
      // Map backend DTO into reusable TransactionInterface
      this.transactions = data.map((tran) => ({
        Id: tran.id,
        title: tran.title,
        description: tran.description,
        amount: tran.amount,
        balance: tran.balance,
        categoryName: tran.categoryName,
        date: tran.date,
        type: tran.type,
      }));
    });
  }
  totalIncome!: number;
  totalExpense!: number;
  Balance!: number;
  // summary!: SummaryDetails;

  GetSummary() {
    this.serv.GetSummary().subscribe((data: any) => {
      this.totalIncome = data.totalIncome;
      this.totalExpense = data.totalExpense;
      this.Balance = data.balance;

      // this.summary.totalExpense = data.TotalExpense;
      // this.summary.totalIncome = data.TotalIncome;
      // this.summary.Balance = data.balance;

      //checking
      // if (
      //   this.summary.totalExpense &&
      //   this.summary.Balance &&
      //   this.summary.totalIncome
      // ) {
      //   console.log(
      //     `${this.summary.totalIncome}  expense: ${this.summary.totalExpense} && balance: ${this.summary.Balance}`
      //   );
      // } else {
      //   console.log('data is not loaded ');
      // }
    });
  }
  applyFilters() {
    this.loadTransactions();
    console.log(this.filters);
  }
}
export interface SummaryDetails {
  totalIncome: number;
  totalExpense: number;
  Balance: number;
}

export interface TransactionDTO {
  id: number;
  amount: number;
  type: string;
  title: string;
  description?: string;
  date?: Date;
  balance?: number;
  categoryName: string;
}
