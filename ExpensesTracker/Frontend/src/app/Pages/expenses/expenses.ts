import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { ExpenseService } from '../../services/expense-service';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../services/category-service';
import { TypeofExpression } from '@angular/compiler';
@Component({
  selector: 'app-expenses',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './expenses.html',
  styleUrl: './expenses.css',
})
export class Expenses {
  categoryName!: CategoryInterface[];

  newExpenseForm = false;

  openForm() {
    this.newExpenseForm = true;
  }
  closeForm() {
    this.newExpenseForm = false;
  }
  constructor(
    private service: ExpenseService,
    private category: CategoryService
  ) {}
  AddNewExpense(newExpense: any) {
    this.service.PushExpenses(newExpense).subscribe({
      next: (res) => {
        alert(`added ${newExpense.title} successfully ${res}`);
      },
      error(err) {
        alert('Issue exists');
      },
    });
    this.closeForm();
    this.loadExpenses();
  }

  expenses!: any;
  filters = {
    categoryName: '',
    startdate: '',
    endDate: '',
    title: '', // used for search
    minAmount: '',
    maxAmount: '',
    search: '',
  };
  ngOnInit() {
    this.loadExpenses();
  }
  loadExpenses() {
    this.service.GetExpenses(this.filters).subscribe((data: any) => {
      this.expenses = data;
      this.category.GetCategories().subscribe((data: any) => {
        this.categoryName = data;
      });

      // console.log('checking expenses');
      // for (let i = 0; i < this.expenses.length; i++) {
      //   console.log(this.expenses[i].title); // should return each row
      // }
    });
  }
  applyFilters() {
    this.loadExpenses();
    console.log(this.filters);
  }
}
export interface CategoryInterface {
  id: number;
  categoryName: string;
  expenses: null;
}
