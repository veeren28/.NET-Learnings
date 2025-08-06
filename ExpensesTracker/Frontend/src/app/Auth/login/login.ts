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
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm!: FormGroup;
  constructor(private authService: AuthService) {}
  ngOnInit() {
    this.loginForm = new FormGroup({
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
    });
  }
  // getter functions for Email and password in fromgruoup

  get Email() {
    return this.loginForm.get('Email');
  }

  get Password() {
    return this.loginForm.get('Password');
  }

  OnLogin(): void {
    if (this.loginForm.value.invalid) {
      console.log('Form is Invalid');
      return;
    }
    this.authService.Login(this.loginForm.value).subscribe({
      next: (res: any) => {
        console.log('Token added succesfully');
        localStorage.setItem('token', res);
        console.log(res);
      },
      error: (err) => {
        console.log('Login Failed' + err);
      },
    });
  }
}
