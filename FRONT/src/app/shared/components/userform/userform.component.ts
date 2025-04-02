import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-userform',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule, 
    RouterModule
  ],
  templateUrl: './userform.component.html',
  styleUrls: ['./userform.component.css'],
  animations: [
    trigger('panelAnimation', [
      state('login', style({
        transform: 'translateX(0)',
        opacity: 1
      })),
      state('register', style({
        transform: 'translateX(0)',
        opacity: 1
      })),
      transition('login => register', [
        style({ transform: 'translateX(100%)', opacity: 0 }),
        animate('500ms ease-in-out', style({ transform: 'translateX(0)', opacity: 1 }))
      ]),
      transition('register => login', [
        style({ transform: 'translateX(-100%)', opacity: 0 }),
        animate('500ms ease-in-out', style({ transform: 'translateX(0)', opacity: 1 }))
      ])
    ]),
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
    ]),
    trigger('shake', [
      state('idle', style({
        transform: 'translateX(0)'
      })),
      state('shaking', style({
        transform: 'translateX(0)'
      })),
      transition('idle => shaking', [
        animate('100ms', style({ transform: 'translateX(-10px)' })),
        animate('100ms', style({ transform: 'translateX(10px)' })),
        animate('100ms', style({ transform: 'translateX(-10px)' })),
        animate('100ms', style({ transform: 'translateX(10px)' })),
        animate('100ms', style({ transform: 'translateX(0)' }))
      ])
    ])
  ]
})
export class UserformComponent implements OnInit {
  authForm: FormGroup;
  @Input() isLogin: boolean = true;
  formTitle: string = 'Login';
  formType: string = 'Login';
  submitType: string = 'Entrar';
  animationState: string = 'login';
  errorMessage: string = '';
  shakeState: string = 'idle';
  
  // Variáveis para força da senha
  passwordStrength = 0;
  passwordMessage = '';
  showPassword = false;
  
  // Validações de senha
  hasMinLength = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasSpecialChar = false;

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService, 
    private router: Router
  ) {
    this.authForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required, 
        Validators.minLength(6),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).*$/)
      ]],
      name: ['']
    });
  }

  ngOnInit(): void {
    this.updateFormState();
    
    // Escuta mudanças no campo de senha para atualizar a força
    this.authForm.get('password')?.valueChanges.subscribe(password => {
      this.checkPasswordStrength(password);
    });
  }

  updateFormState() {
    this.formTitle = this.isLogin ? 'Cadastro' : 'Login';
    this.formType = this.isLogin ? 'Login' : 'Cadastrar';
    this.submitType = this.isLogin ? 'Entrar' : 'Cadastrar';
    this.animationState = this.isLogin ? 'login' : 'register';
    this.errorMessage = '';
    this.passwordStrength = 0;
    this.passwordMessage = '';

    if (this.isLogin) {
      this.authForm.get('name')?.clearValidators();
      this.authForm.get('password')?.setValidators([Validators.required]);
    } else {
      this.authForm.get('name')?.setValidators([Validators.required]);
      this.authForm.get('password')?.setValidators([
        Validators.required, 
        Validators.minLength(6),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).*$/)
      ]);
    }
    this.authForm.get('name')?.updateValueAndValidity();
    this.authForm.get('password')?.updateValueAndValidity();
  }

  checkPasswordStrength(password: string) {
    // Reiniciar validações
    this.hasMinLength = false;
    this.hasUpperCase = false;
    this.hasLowerCase = false;
    this.hasSpecialChar = false;
    
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

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  navigateToPage() {
    this.isLogin = !this.isLogin;
    this.updateFormState();
    this.authForm.reset();
  }

  shakeForm() {
    this.shakeState = 'shaking';
    setTimeout(() => {
      this.shakeState = 'idle';
    }, 500);
  }

  onSubmit() {
    if (this.authForm.valid) {
      const formData = this.authForm.value;

      if (this.isLogin) {
        this.authService.login(formData.email, formData.password).subscribe({
          next: () => this.router.navigate(['/home']),
          error: (err) => {
            console.error('Erro de login', err);
            
            if (err.status === 404) {
              this.errorMessage = 'Usuário não encontrado. Verifique seu email ou senha.';
            } else if (err.status === 401) {
              this.errorMessage = 'Email ou senha incorretos. Tente novamente.';
            } else {
              this.errorMessage = 'Ocorreu um erro ao fazer login. Por favor, tente novamente.';
            }
            
            this.shakeForm();
          }
        });
      } else {
        this.authService.register(formData).subscribe({
          next: () => {
            this.router.navigate(['/login']);
            this.isLogin = true;
            this.updateFormState();
          },
          error: (err) => {
            console.error('Erro de registro', err);
            
            if (err.status === 409) {
              this.errorMessage = 'Este email já está em uso. Tente outro email.';
            } else {
              this.errorMessage = 'Ocorreu um erro ao criar a conta. Por favor, tente novamente.';
            }
            
            this.shakeForm();
          }
        });
      }
    }
  }

  clearError() {
    this.errorMessage = '';
  }
}