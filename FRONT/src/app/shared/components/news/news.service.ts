import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { news } from '../../../interfaces/news';
import { Observable } from 'rxjs';
import { RegisterNews } from '../../../interfaces/RegisterNews';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  
  GetById(id: number): Observable<news> {
    const url = `${this.API}/News/${id}`;

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<news>(url, { headers }); 
  }

  updateNews(formData: FormData,id?: number): Observable<news> {
    console.log('FormData conteúdo:');
  for (let [key, value] of (formData as any).entries()) {
    console.log(`${key}: ${value}`);
  }
    const url = `${this.API}/News/${id}`;

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.put<news>(url, formData, { headers }); 
  }

  private readonly API = 'http://localhost:5197';
  constructor(private http: HttpClient) { }
  
  GetAll() :Observable<news[]>
  {
    const url = `${this.API}/News`;
    return this.http.get<news[]>(url);
    
  }

  Post(news: FormData): Observable<news> {
    const url = `${this.API}/News`;

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post<news>(url, news, { headers }); 
  }

  GetByUser(): Observable<news[]> {
    const url = `${this.API}/News/get-by-user`;

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<news[]>(url, { headers }); 
  }

  Delete(id: number): Observable<void> 
  {
    const url = `${this.API}/News/${id}`;

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.delete<void>(url, { headers });
  }

  GetNewsMoreLiked(): Observable<news> {
    const url = `${this.API}/Like`;

    return this.http.get<news>(url);
  }

  DoLike(id: number): Observable<void> {
    const url = `${this.API}/Like/${id}`;
  
    const token = localStorage.getItem('token');

    console.log(token);
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post<void>(url, id,{ headers }); 
  }
}
