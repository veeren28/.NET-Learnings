// auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('token'); //because only after login token is stored

  if (token) {
    return true; // ✅ Allow route
  }

  alert('You must log in to access this page');
  router.navigate(['/Login']);
  return false; // ❌ Block route
};
