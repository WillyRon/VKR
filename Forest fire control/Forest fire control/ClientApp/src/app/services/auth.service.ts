import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../models/user.model';
import { jwtDecode } from 'jwt-decode';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth';
  private isAdminSubject = new BehaviorSubject<boolean>(false);
  isAdmin$: Observable<boolean> = this.isAdminSubject.asObservable();
  private emailSubject = new BehaviorSubject<string>('');
  email$: Observable<string> = this.emailSubject.asObservable();
 
  constructor(private http: HttpClient) { 
    this.updateTokenStatus();
  }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };

    return this.http.post<any>(`${this.apiUrl}/login`, loginData)
      .pipe(
        tap(response => {
          if (response && response.token) {
            localStorage.setItem('token', response.token);
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

  updateTokenStatus(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.isAdminSubject.next(decodedToken.role === 'Admin');
      this.emailSubject.next(decodedToken.email);
    }
  }

  isAdmin(): Observable<boolean> {
    return this.isAdmin$;
  }

  getEmail(): string {
    return this.emailSubject.value;
  }

  logout(): void {
    localStorage.removeItem('token');
    this.isAdminSubject.next(false);
  }
}

