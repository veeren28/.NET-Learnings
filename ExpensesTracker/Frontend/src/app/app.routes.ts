import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';
import { Incomes } from './Pages/incomes/incomes';
import { Expenses } from './Pages/expenses/expenses';
import { Dashboard } from './Pages/dashboard/dashboard';

export const routes: Routes = [
  { path: '', component: Dashboard },
  { path: 'Incomes', component: Incomes },
  { path: 'Expenses', component: Expenses },
];
