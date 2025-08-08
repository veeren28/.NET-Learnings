import { Component } from '@angular/core';

import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  Validators,
  FormBuilder,
  FormsModule,
} from '@angular/forms';
import { AuthService } from '../../auth-service';
import { CommonModule } from '@angular/common';
import { errorContext } from 'rxjs/internal/util/errorContext';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm!: FormGroup;

  constructor(private authService: AuthService, private route: Router) {}

  ngOnInit() {
    this.loginForm = new FormGroup({
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }

  get Email() {
    return this.loginForm.get('Email');
  }

  get Password() {
    return this.loginForm.get('Password');
  }

  OnLogin(): void {
    this.loginForm.markAllAsTouched();

    if (this.loginForm.invalid) {
      console.log('Form is Invalid');
      return;
    }

    this.authService.Login(this.loginForm.value).subscribe({
      next: (res: any) => {
        console.log('✅ Login successful:', res);
        localStorage.setItem('token', res.token); // <-- Important
        window.alert('Logged in successfully');
        this.route.navigate(['/Dashboard']);
      },
      error: (err) => {
        console.log('❌ Login failed:', err);
        if (err.error && err.error.message) {
          window.alert(err.error.message);
        } else {
          window.alert(`Login Failed ${err}`);
        }
      },
    });
  }
}
