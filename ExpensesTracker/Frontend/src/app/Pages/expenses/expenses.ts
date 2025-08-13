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
  transactions!: any;
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
    console.log('Calling GetExpenses...');
    this.service.GetExpenses(this.filters).subscribe((data: any) => {
      console.log('Got expenses:', data);
      this.transactions = data;
    });
  }
  applyFilters() {
    this.loadExpenses();
    console.log(this.filters);
  }
}
