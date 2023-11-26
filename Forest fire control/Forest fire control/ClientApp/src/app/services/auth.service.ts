import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { User } from '../models/user.model';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth';
  private isAdminSubject = new BehaviorSubject<boolean>(false);
  isAdmin$: Observable<boolean> = this.isAdminSubject.asObservable();
 
  constructor(private http: HttpClient) { 
    this.updateAdminStatus();
  }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };

    return this.http.post<any>(`${this.apiUrl}/login`, loginData)
      .pipe(
        tap(response => {
          if (response && response.token) {
            localStorage.setItem('token', response.token);
            localStorage.setItem('email', email);
          }
        })
      );
  }
  addUser(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}/create-user`, user)
  }

  getUserInfo(email: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/user/${email}`);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
  updateAdminStatus(): void {
    this.getUserInfo(localStorage.getItem('email')).subscribe(
      response => {
        this.isAdminSubject.next(response.role === 2);
      }
    );
  }
  isAdmin(): Observable<boolean> {
    return this.isAdmin$;
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this.isAdminSubject.next(false);
  }
}

