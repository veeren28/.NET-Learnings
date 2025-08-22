import { Component } from '@angular/core';
import {
  TransactionInterface,
  TrasnsactionCard,
} from '../../Components/trasnaction-card/trasnsaction-card';
import { IncomeService } from '../../services/income-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators'; // ✅ import filter

@Component({
  selector: 'app-incomes',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './incomes.html',
  styleUrl: './incomes.css',
})
export class Incomes {
  incomes!: any[];
  newIncomeForm = false;

  filters = {
    categoryName: '',
    startDate: '',
    endDate: '',
    minAmount: '',
    maxAmount: '',
    title: '',
  };

  constructor(private service: IncomeService, private router: Router) {} // ✅ inject Router

  ngOnInit() {
    // run on first load
    this.loadIncome();
  }
  income!: TransactionInterface[];

  openForm() {
    this.newIncomeForm = true;
  }

  closeForm() {
    this.newIncomeForm = false;
  }

  loadIncome() {
    this.service.Get(this.filters).subscribe((data: any) => {
      this.incomes = data;
      console.log('service is executed');
    });
  }

  applyFilters() {
    this.loadIncome();
    console.log(this.filters);
  }

  AddNewIncome(IncomeForm: any) {
    this.service.Post(IncomeForm).subscribe({
      next: (res) => {
        console.log(`Income added ${res}`);
        console.log(IncomeForm);
      },
      error(err) {
        console.log(`${err}`);
      },
    });
    alert('income added');
    this.closeForm();
    this.loadIncome();
  }
}
