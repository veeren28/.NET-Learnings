import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';
import { Incomes } from './Pages/incomes/incomes';
import { Expenses } from './Pages/expenses/expenses';
import { Dashboard } from './Pages/dashboard/dashboard';
import { Login } from './Auth/login/login';
import { Register } from './Auth/register/register';
import { InvalidComponent } from './invalid-component/invalid-component';
import { authGuard } from './auth-guard';
import { ParentComponent } from './parent-component/parent-component';
import { NgModel } from '@angular/forms';
export const routes: Routes = [
  { path: '', redirectTo: 'Login', pathMatch: 'full' },
  { path: 'Login', component: Login },
  { path: 'RegisterUser', component: Register },
  // { path: 'Dashboard', component: Dashboard, canActivate: [authGuard] },
  // { path: 'Incomes', component: Incomes },
  // { path: 'Expenses', component: Expenses },
  // { path: 'Invalid', component: InvalidComponent },
  {
    path: 'home',
    component: ParentComponent,
    children: [
      { path: '', component: Dashboard },
      { path: 'Dashboard', component: Dashboard },
      { path: 'Incomes', component: Incomes },
      { path: 'Expenses', component: Expenses },
      // { path: 'Invalid', component: InvalidComponent },
    ],
    canActivate: [authGuard],
  },
];
