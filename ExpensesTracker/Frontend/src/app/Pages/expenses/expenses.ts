import { Component } from '@angular/core';
import {
  TransactionInterface,
  TrasnsactionCard,
} from '../../Components/trasnaction-card/trasnsaction-card';
import { ExpenseService } from '../../services/expense-service';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../services/category-service';
import { TypeofExpression } from '@angular/compiler';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-expenses',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './expenses.html',
  styleUrl: './expenses.css',
})
export class Expenses {
  categoryName!: CategoryInterface[];

  newExpenseForm = false;

  ///Adding expense Logic
  openForm() {
    this.newExpenseForm = true;
  }
  closeForm() {
    this.newExpenseForm = false;
  }
  constructor(
    private service: ExpenseService,
    private category: CategoryService,
    private router: Router
  ) {}
  AddNewExpense(newExpense: any) {
    this.service.PushExpenses(newExpense).subscribe({
      next: (res) => {
        alert(`added ${newExpense.title} successfully ${res}`);
        this.closeForm();
        this.loadExpenses();
      },
      error(err) {
        alert('Issue exists');
      },
    });
  }

  expenses!: TransactionInterface[];
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

  LoadExpensesCount = 0;
  //loading Expenses
  loadExpenses() {
    this.LoadExpensesCount++;

    this.service.GetExpenses(this.filters).subscribe((data: ExpensesDTO[]) => {
      // Map backend DTO into reusable TransactionInterface
      this.expenses = data.map((exp) => ({
        Id: exp.expenseId,
        title: exp.title,
        description: exp.description,
        amount: exp.amount,
        date: exp.date,
        balance: exp.balance,
        categoryName: exp.categoryName,
      }));
      console.log(`laod Expense Count: ${this.LoadExpensesCount}`);
      this.category.GetCategories().subscribe((data: any) => {
        this.categoryName = data;
      });
    });
  }

  applyFilters() {
    this.loadExpenses();
    console.log(this.filters);
  }
  expenseToDelete: any = null;

  confirmDelete(expens: TransactionInterface) {
    console.log(expens);
    this.expenseToDelete = expens;
    console.log('confirmDelete performed');
  }
  deleteExpenseConfirmed() {
    if (!this.expenseToDelete) return;

    this.service.DeleteExpense(this.expenseToDelete.expenseId).subscribe({
      next: (res) => {
        console.log(`Deleted item: ${this.expenseToDelete?.title}`);
        this.expenseToDelete = null; // Close modal
        this.loadExpenses(); // Refresh list after deletion
      },
      error: (err) => {
        console.log('Issue in deleting expense', err);
      },
      complete: () => {
        console.log('Complete Statement');
      },
    });
  }

  // editing Expense:
  expenseToEdit: any = null;

  confirmEdit(expense: TransactionInterface) {
    this.expenseToEdit = expense;
    console.log('confirmEdit performed');
  }

  // Called when the user submits the edit form
  editExpenseConfirmed() {
    if (!this.expenseToEdit) return;

    this.service
      .EditExpense(this.expenseToEdit.expenseId, this.expenseToEdit)
      .subscribe({
        next: (res) => {
          console.log('Edited Item: ' + this.expenseToEdit.title);
          console.log('hello');
          this.loadExpenses(); // Refresh list AFTER successful edit
          this.expenseToEdit = null; // Close modal
        },
        error: (err) => {
          console.log('Error editing expense', err);
        },
      });
  }
}

export interface CategoryInterface {
  id: number;
  categoryName: string;
  expenses: null;
}
export interface ExpensesDTO {
  expenseId: number;
  amount: number;
  title: string;
  description: string;
  date: Date; // or Date if you want to convert
  balance: number;
  categoryName?: string;
  categoryId?: number;
}
export interface UpdateExpenseDTO {
  id: number;
  title: string;
  amount: number;
  description: string;
  categoryName: string;
  date: Date;
}
