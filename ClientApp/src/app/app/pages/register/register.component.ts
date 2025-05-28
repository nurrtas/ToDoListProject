import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ]
})
export class RegisterComponent {
  registerForm: FormGroup;
  showPassword = false;
  mesaj = '';

  constructor(private fb: FormBuilder, public router: Router) {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  kayitOl() {
    if (this.registerForm.valid) {
      console.log('Kayıt verisi:', this.registerForm.value);
      this.mesaj = '✅ Kayıt başarılı!';
      // Örn: backend'e gönderim yapılabilir
    } else {
      this.mesaj = '⚠️ Lütfen tüm alanları doldurun.';
    }
  }
}
