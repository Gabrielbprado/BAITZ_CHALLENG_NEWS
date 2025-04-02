import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService, User } from '../../auth.service';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-register-components',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register-components.component.html',
  styleUrl: './register-components.component.css',
  animations: [
    trigger('errorAnimation', [
      state('void', style({
        opacity: 0,
        transform: 'translateY(-20px)'
      })),
      state('*', style({
        opacity: 1,
        transform: 'translateY(0)'
      })),
      transition('void => *', [
        animate('300ms ease-out')
      ]),
      transition('* => void', [
        animate('200ms ease-in')
      ])
    ])
  ]
})
export class RegisterComponentsComponent {
  registerForm: FormGroup;
  errorMessage: string = '';
  isCollaborator: boolean = true;
  showPassword: boolean = false;
  
  // Variáveis para força da senha
  passwordStrength = 0;
  passwordMessage = '';
  
  // Validações de senha
  hasMinLength = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasSpecialChar = false;

  constructor(
    private authService: AuthService, 
    private router: Router,
    private fb: FormBuilder
  ) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
    
    // Monitorar mudanças na senha
    this.registerForm.get('password')?.valueChanges.subscribe(password => {
      this.checkPasswordStrength(password);
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  checkPasswordStrength(password: string) {
    if (!password) {
      this.passwordStrength = 0;
      this.passwordMessage = '';
      return;
    }

    // Verificar critérios
    this.hasMinLength = password.length >= 6;
    this.hasUpperCase = /[A-Z]/.test(password);
    this.hasLowerCase = /[a-z]/.test(password);
    this.hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);
    
    // Calcular força (0-100)
    let strength = 0;
    
    // Comprimento básico
    if (password.length >= 6) strength += 25;
    if (password.length >= 8) strength += 10;
    if (password.length >= 10) strength += 10;
    
    // Complexidade
    if (this.hasUpperCase) strength += 15;
    if (this.hasLowerCase) strength += 15;
    if (this.hasSpecialChar) strength += 25;
    
    // Limitar a 100
    this.passwordStrength = Math.min(100, strength);
    
    // Definir mensagem
    if (this.passwordStrength < 30) {
      this.passwordMessage = 'Fraca';
    } else if (this.passwordStrength < 60) {
      this.passwordMessage = 'Média';
    } else if (this.passwordStrength < 80) {
      this.passwordMessage = 'Boa';
    } else {
      this.passwordMessage = 'Forte';
    }
  }

  register() {
    if (this.registerForm.invalid) {
      return;
    }
    
    const formValues = this.registerForm.value;
    
    if (formValues.password !== formValues.confirmPassword) {
      this.errorMessage = 'As senhas não coincidem';
      return;
    }

    const user: User = {
    name: formValues.name,
    email: formValues.email,
    password: formValues.password
    }

    this.authService.register(user).subscribe({
      next: (response) => {
        console.log('Colaborador registrado com sucesso', response);
        this.router.navigate(['/profile']);
      },
      error: (error) => {
        console.error('Erro ao registrar colaborador', error);
        this.errorMessage = error.error.message || 'Erro ao registrar colaborador';
      }
    });
  }
}