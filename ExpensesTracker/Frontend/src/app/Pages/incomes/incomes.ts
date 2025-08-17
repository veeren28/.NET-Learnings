import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
import { IncomeService } from '../../services/income-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-incomes',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './incomes.html',
  styleUrl: './incomes.css',
})
export class Incomes {
  constructor(private service: IncomeService) {}
  incomes: any;
  filters = {
    categoryName: '',
    startdate: '',
    endDate: '',
    minAmount: '',
    maxAmount: '',
    search: '',
  };
  ngOnInit() {
    this.loadIncome();
  }
  loadIncome() {
    this.service.Get(this.filters).subscribe((data: any) => {
      this.incomes = data;
      console.log('checking ');
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
}
