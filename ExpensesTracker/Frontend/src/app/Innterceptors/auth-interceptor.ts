import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  // URLs that don’t need authentication
  const publicUrls = ['/api/auth/Login', '/api/auth/Register'];

  // Skip adding token if request is public
  if (publicUrls.some((url) => req.url.includes(url))) {
    return next(req);
  }

  // If token is missing → block request
  if (!token) {
    window.alert('Unauthorized to access this feature');
    // router.navigate(['/Invalid']);
    return throwError(
      () => new HttpErrorResponse({ status: 401, statusText: 'No auth token' })
    );
  }

  // If token exists → attach it to request
  const clone = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });

  return next(clone);
};
export const testInterceptor: HttpInterceptorFn = (req, next) => {
  console.log('🚀 Test interceptor is running for URL:', req.url);
  return next(req);
};
