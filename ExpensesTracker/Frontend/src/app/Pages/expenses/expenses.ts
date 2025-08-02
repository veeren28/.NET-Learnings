import { Component } from '@angular/core';
import { TrasnsactionCard } from '../../Components/trasnaction-card/trasnsaction-card';
@Component({
  selector: 'app-expenses',
  imports: [TrasnsactionCard],
  templateUrl: './expenses.html',
  styleUrl: './expenses.css',
})
export class Expenses {}
