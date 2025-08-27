import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Expenses, ExpensesDTO } from '../../Pages/expenses/expenses';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-trasnsaction-card',
  imports: [CommonModule],
  templateUrl: './trasnsaction-card.html',
  styleUrl: './trasnsaction-card.css',
})
export class TrasnsactionCard {
  @Input() transactions!: TransactionInterface;

  @Output() deleteEvent = new EventEmitter();
  @Output() editEvent = new EventEmitter();

  onDelete() {
    this.deleteEvent.emit(this.transactions);
    console.log(
      `Emitted the ${this.transactions.Id} ${this.transactions.title}for delete from transaction card`
    );
  }
  onEdit() {
    this.editEvent.emit(this.transactions);
  }
  ngOnInit() {
    console.log(this.transactions.type);
    console.log('from Transacrtion card');
  }
}

export interface TransactionInterface {
  Id?: number; // can be used for both income and expense;
  title?: string;
  description?: string;
  amount?: number;
  date?: Date;
  updatedAt?: Date;
  balance?: number;

  //transaction specific field
  type?: string;
  //expenses specific field
  categoryName?: string;
}
