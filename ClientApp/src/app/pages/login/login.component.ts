import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, LoginRequest } from '../../services/auth.service';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class LoginComponent {
  loginForm: FormGroup;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  girisYap(): void {
    if (this.loginForm.valid) {
      const credentials: LoginRequest = {
        userName: this.loginForm.get('userName')?.value,
        password: this.loginForm.get('password')?.value
      };

      console.log('Gönderilen login verisi:', credentials); // 👈 Giriş verisini gör

      this.authService.login(credentials).subscribe({
        next: (res) => {
          console.log('Login başarılı, token alındı:', res.token); // 👈 Başarıyı göster
          this.authService.setToken(res.token);
          this.router.navigate(['/todo']);
        },
        error: (err) => {
          alert('Giriş başarısız. Bilgilerinizi kontrol ediniz.');
          console.error('Login hatası:', err); // 👈 Backend hatasını gör
        }
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  navigateToRegister(): void {
    this.router.navigate(['/register']);
  }
}
