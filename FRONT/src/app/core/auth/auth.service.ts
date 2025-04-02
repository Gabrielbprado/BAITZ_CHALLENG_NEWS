import { Injectable } from '@angular/core';
import { HttpClient,HttpClientModule } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Console } from 'node:console';

export interface User {
  email: string;
  password: string;
  name?: string; 
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5197'; 

  constructor(private http: HttpClient) {}

  register(user: User): Observable<User> {
    console.log('Metodo Cadastro');

    return this.http.post<User>(`${this.apiUrl}/Customer`, user);
  }

  login(email: string, password: string): Observable<{ name: string; token: { accessToken: string }}> {
    console.log('Metodo login');
    return this.http.post<{ name: string; token: { accessToken: string } }>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token.accessToken);
        })
      );
  }
  

}
