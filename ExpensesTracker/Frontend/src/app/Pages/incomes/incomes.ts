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
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-incomes',
  imports: [TrasnsactionCard, FormsModule, CommonModule],
  templateUrl: './incomes.html',
  styleUrl: './incomes.css',
})
export class Incomes {
  // newIncomeForm = false;

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

  loadIncome() {
    this.service.Get(this.filters).subscribe((data: any) => {
      this.income = data.map((inc: IncomeDTO) => ({
        Id: inc.incomeId,
        title: inc.title,
        amount: inc.amount,
        description: inc.description,
        balace: inc.balance,
      }));
      console.log('service is executed');
    });
  }

  applyFilters() {
    this.loadIncome();
    console.log(this.filters);
  }

  // Adding Income
  newIncomeForm = false;
  openForm() {
    this.newIncomeForm = true;
  }

  closeForm() {
    this.newIncomeForm = false;
  }
  AddNewIncome(IncomeForm: any) {
    this.service.Post(IncomeForm).subscribe({
      next: (res) => {
        console.log(`Income added ${res}`);
        console.log(IncomeForm);
        this.closeForm();
        this.loadIncome();
      },
      error(err) {
        console.log(`${err}`);
      },
    });
    alert('income added');
  }

  // deleting Income
  incomeToDelete: any = null;

  confirmDeleteIncome(del: any) {
    console.log(`income ${del.title} will be deleted`);
    this.incomeToDelete = del;
  }

  deleteConfirm() {
    this.service.Delete(this.incomeToDelete.Id).subscribe({
      next: () => {
        alert(`${this.incomeToDelete.title} successfully deleted`);
        this.incomeToDelete = null;
        this.loadIncome();
      },
      error(err) {
        console.log('Error in DeleteIncome');
      },
    });
  }
  incomeToEdit: any = null;
  //income from transaction card
  confirmEditIncome(incomess: TransactionInterface) {
    this.incomeToEdit = incomess;
  }

  editIncomeConfirmed() {
    this.service.Edit(this.incomeToEdit.Id, this.incomeToEdit).subscribe({
      next: () => {
        console.log(`${this.incomeToEdit} is edited`);
        this.incomeToEdit = null;
        this.loadIncome();
      },
      error(err) {
        console.log('error in editIncome');
      },
    });
  }
}

export interface IncomeDTO {
  incomeId: number;
  amount: number;
  description: string;

  title: string;
  balance: number;
}
