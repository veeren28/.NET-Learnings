import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { ExpenseService } from '../../services/expense-service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-expenses',
  imports: [TrasnsactionCard, FormsModule],
  templateUrl: './expenses.html',
  styleUrl: './expenses.css',
})
export class Expenses {
  constructor(private service: ExpenseService) {}
  expenses: any[] = [];
  filters = {
    categoryName: '',
    startdate: '',
    endDate: '',
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

      console.log('checking expenses');
      for (let i = 0; i < this.expenses.length; i++) {
        console.log(this.expenses[i].title); // should return each row
      }
    });
  }
  applyFilters() {
    this.loadExpenses();
    console.log(this.filters);
  }
}
