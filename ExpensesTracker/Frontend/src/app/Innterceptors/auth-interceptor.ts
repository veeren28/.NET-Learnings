import {
  HTTP_INTERCEPTORS,
  HttpEventType,
  HttpHandler,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { inject } from '@angular/core'; // ✅ Correct casing: 'inject', not 'Inject'
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

// Functional interceptor (Angular 15+ style)
// This runs for every outgoing HTTP request in the app
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // ✅ Functional interceptors don't have constructors, so we use 'inject()'
  const router = inject(Router);

  // Fetch the stored token (JWT or similar) from local storage
  const token = localStorage.getItem('token');

  // If token is missing, block or redirect
  if (!token) {
    window.alert('Unauthorized to access this feature'); // Show alert to user
    router.navigate(['/Invalid']); // Redirect to 'Invalid' page
    return next(req);
    // ⚠ Currently still sending the request without token —
    // could replace with EMPTY to fully block request
  }

  // Clone the original request and attach Authorization header
  const clone = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`, // Append Bearer token
    },
  });

  // Pass the modified request down the chain
  return next(clone);
};
