import {
  AbstractControl,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

export function passwordMatchValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('Password')?.value;
    const confirmPassword = control.get('ConfirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  };
}
import { ParseSourceFile } from '@angular/compiler';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  RegisterForm!: FormGroup;
  formErrors: string[] = [];
  successMessage = ' ';

  constructor(private registeruser: AuthService, private router: Router) {}

  ngOnInit() {
    this.RegisterForm = new FormGroup(
      {
        Email: new FormControl('', [Validators.required, Validators.email]),
        Username: new FormControl('', [
          Validators.required,
          Validators.minLength(5),
        ]),
        Balance: new FormControl('', [
          Validators.required,
          Validators.min(1),
          Validators.max(20000000),
        ]),
        Password: new FormControl('', [
          Validators.required,
          Validators.minLength(8),
        ]),
        ConfirmPassword: new FormControl('', [
          Validators.required,

          Validators.minLength(8),
        ]),
      },
      { validators: passwordMatchValidator() }
    );
  }
  get Email() {
    return this.RegisterForm.get('Email');
  }
  get Username() {
    return this.RegisterForm.get('Username');
  }
  get Balance() {
    return this.RegisterForm.get('Balance');
  }
  get Password() {
    return this.RegisterForm.get('Password');
  }
  get ConfirmPassword() {
    return this.RegisterForm.get('ConfirmPassword');
  }

  // OnRegister(): void {
  //   // this.formErrors = [];
  //   this.RegisterForm.markAllAsTouched();

  //   if (this.RegisterForm.invalid) {
  //     console.log('Register Form is Invalid');
  //     // this.RegisterForm.markAllAsTouched();

  //     return;
  //   }
  //   this.registeruser.Register(this.RegisterForm.value).subscribe({
  //     next: (res: any) => {
  //       console.log('Registered successfully');
  //     },
  //     error: (err) => {
  //       if (err?.error && Array.isArray(err.error)) {
  //         this.formErrors = err.error.map((e: any) => e.description);
  //       }
  //     },
  //   });
  // }

  OnRegister(): void {
    this.successMessage = '';
    this.formErrors = [];
    if (this.RegisterForm.invalid) {
      console.log('Invalid Form');
      return;
    }
    this.registeruser.Register(this.RegisterForm.value).subscribe({
      next: (res: any) => {
        // i want to show the message of backend.
        console.log('✅ Success response from backend:', res);
        this.successMessage =
          res.message || res.description || 'Registered successfully!';
        window.alert(this.successMessage);
        setTimeout(() => {
          this.router.navigate(['/Login']);
        }, 1500);
        this.RegisterForm.reset();
      },
      error: (err) => {
        console.log('Error Object:', JSON.stringify(err, null, 2));
        // window.alert(err.errors.error);
      },
    });
  }
}
