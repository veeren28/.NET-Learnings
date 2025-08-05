import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';
import { Incomes } from './Pages/incomes/incomes';
import { Expenses } from './Pages/expenses/expenses';
import { Dashboard } from './Pages/dashboard/dashboard';
import { Login } from './Auth/login/login';
import { Register } from './Auth/register/register';

export const routes: Routes = [
  { path: '', redirectTo: 'Login', pathMatch: 'full' },
  { path: 'Login', component: Login },
  { path: 'RegisterUser', component: Register },
  { path: 'Dashboard', component: Dashboard },
  { path: 'Incomes', component: Incomes },
  { path: 'Expenses', component: Expenses },
];
