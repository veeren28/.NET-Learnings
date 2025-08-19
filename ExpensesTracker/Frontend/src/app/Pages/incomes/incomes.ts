import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { IncomeService } from '../../services/income-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-incomes',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './incomes.html',
  styleUrl: './incomes.css',
})
export class Incomes {
  constructor(private service: IncomeService) {}
  incomes!: any[];
  newIncomeForm = false;

  openForm() {
    this.newIncomeForm = true;
  }

  closeForm() {
    this.newIncomeForm = false;
  }
  filters = {
    categoryName: '',
    startDate: '',
    endDate: '',
    minAmount: '',
    maxAmount: '',
    title: '', // used for search
  };
  ngOnInit() {
    this.hello();
    this.loadIncome();
  }
  hello() {
    console.log('hello');
  }
  loadIncome() {
    this.service.Get(this.filters).subscribe((data: any) => {
      this.incomes = data;
      console.log('service is executed ');
      for (let i of this.incomes) {
        console.log(i.title);
      }
      console.log(data);
    });
  }
  applyFilters() {
    this.loadIncome();
    console.log(this.filters);
  }

  AddNewIncome(IncomeForm: any) {
    this.service.Post(IncomeForm).subscribe({
      next: (res) => {
        console.log(`Income added ${res.valueOf}`);
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
