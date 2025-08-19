import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-trasnsaction-card',
  imports: [CommonModule],
  templateUrl: './trasnsaction-card.html',
  styleUrl: './trasnsaction-card.css',
})
export class TrasnsactionCard {
  @Input() transactions!: TransactionInterface;
  ngOnInit() {
    console.log(`${this.transactions.title}   date: ${this.transactions.date}`);
  }
}

export interface TransactionInterface {
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
